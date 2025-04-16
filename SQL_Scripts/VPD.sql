CONN admin/06092004@localhost:1521/DaiHocX;
ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;

-- Câu 1:
-- 1. Tạo VPD Function cho bảng SINHVIEN
CREATE OR REPLACE FUNCTION vpd_sinhvien (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
) RETURN VARCHAR2
IS
    v_role  VARCHAR2(30);
    v_khoa  VARCHAR2(10);
BEGIN
    IF USER = 'ADMIN' THEN
        RETURN '1=1'; -- admin thấy hết
    END IF;

    SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANLĐ = USER;

    IF v_role = 'GV' THEN
        SELECT MAĐV INTO v_khoa FROM NHANVIEN WHERE MANLĐ = USER;
        RETURN 'KHOA = ''' || v_khoa || '''';

    ELSIF v_role = 'NV CTSV' THEN
        RETURN '1=1';  -- thấy và sửa toàn bộ SV

    ELSIF v_role = 'NV PĐT' THEN
        RETURN '1=1'; 

    ELSE
        RETURN 'MASV = ''' || USER || ''''; -- SV chỉ thấy mình
    END IF;

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN 'MASV = ''' || USER || '''';
END;
/

-- 2. Áp dụng VPD cho bảng SINHVIEN,"
CONN admin/06092004@localhost:1521/DaiHocX;

BEGIN
  DBMS_RLS.ADD_POLICY (
    object_schema   => 'ADMIN',
    object_name     => 'SINHVIEN',
    policy_name     => 'POLICY_SINHVIEN',
    function_schema => 'ADMIN',
    policy_function => 'VPD_SINHVIEN',
    statement_types => 'SELECT, INSERT, UPDATE, DELETE',
    update_check    =>  TRUE
  );
END;
/



-- Kiểm tra cột TINHTRANG của SINHVIEN
conn admin/06092004@localhost:1521/DaiHocX
CREATE OR REPLACE TRIGGER trg_check_update_tinhtrang
BEFORE UPDATE OF TINHTRANG ON SINHVIEN
FOR EACH ROW
DECLARE
    v_role VARCHAR2(20);
BEGIN
    SELECT VAITRO INTO v_role FROM NHANVIEN WHERE UPPER(MANLĐ) = UPPER(USER);

    IF v_role != 'NV PĐT' THEN
        RAISE_APPLICATION_ERROR(-20003, 'Chỉ NV PĐT mới được cập nhật tình trạng học vụ');
    END IF;
END;
/

-- Kiểm tra cập nhật thông tin SV
CREATE OR REPLACE TRIGGER trg_check_update_sinhvien
BEFORE UPDATE ON SINHVIEN
FOR EACH ROW
DECLARE
    v_role VARCHAR2(20);
    v_user VARCHAR2(30) := UPPER(USER);
BEGIN
    IF UPDATING('TINHTRANG') THEN
        RETURN; -- xử lý đã nằm ở trigger trg_check_update_tinhtrang
    END IF;

    BEGIN
        SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANLĐ = v_user;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_role := 'SV';
    END;

    IF v_role = 'SV' THEN
        IF :OLD.MASV != v_user THEN
            RAISE_APPLICATION_ERROR(-20004, 'Sinh viên chỉ được cập nhật thông tin của chính mình');
        END IF;

        IF UPDATING('HOTEN') OR UPDATING('PHAI') OR UPDATING('NGSINH') OR
           UPDATING('KHOA') OR UPDATING('TINHTRANG') THEN
            RAISE_APPLICATION_ERROR(-20005, 'Sinh viên chỉ được phép cập nhật ĐCHI và ĐT');
        END IF;

    ELSIF v_role = 'NV CTSV' THEN
        IF UPDATING('TINHTRANG') THEN
            RAISE_APPLICATION_ERROR(-20007, 'NV CTSV không được phép cập nhật tình trạng học vụ');
        END IF;

    ELSIF v_role = 'NV PĐT' THEN
        RAISE_APPLICATION_ERROR(-20008, 'NV PĐT chỉ được phép cập nhật TINHTRANG');

    ELSE
        RAISE_APPLICATION_ERROR(-20006, 'Bạn không có quyền cập nhật thông tin sinh viên');
    END IF;
END;
/

-- Câu 4: 
-- Tạo VPD function cho bảng DANGKY
CREATE OR REPLACE FUNCTION vpd_dangky (
    schema_name IN VARCHAR2,
    table_name  IN VARCHAR2
) RETURN VARCHAR2
IS
    v_role  VARCHAR2(30);
BEGIN
    SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANLĐ = USER;

    IF v_role = 'GV' THEN
        RETURN 'MAMM IN (SELECT MAMM FROM MOMON WHERE MAGV = ''' || USER || ''')';
    ELSIF v_role = 'NV PKT' THEN
        RETURN '1=1';
    ELSIF v_role = 'NV PĐT' THEN
        RETURN '1=1';
    ELSE
        RETURN 'MASV = ''' || USER || '''';
    END IF;

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN 'MASV = ''' || USER || '''';
END;
/

-- Áp dụng VPD cho bảng DANGKY
BEGIN
  DBMS_RLS.ADD_POLICY (
    object_schema   => 'DAIHOCX',
    object_name     => 'DANGKY',
    policy_name     => 'POLICY_DANGKY',
    function_schema => 'DAIHOCX',
    policy_function => 'vpd_dangky',
    statement_types => 'SELECT, INSERT, UPDATE, DELETE',
    update_check    => TRUE
  );
END;
/

/* -- CODE CHUẨN 14 NGÀY THEO ĐỀ
BEGIN
  EXECUTE IMMEDIATE 'DROP FUNCTION is_within_14_days';
EXCEPTION
  WHEN OTHERS THEN
    IF SQLCODE != -4043 THEN -- ORA-04043: object does not exist
      RAISE;
    END IF;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP TRIGGER trg_check_dangky_access';
EXCEPTION
  WHEN OTHERS THEN
    IF SQLCODE != -4080 THEN -- ORA-04080: trigger does not exist
      RAISE;
    END IF;
END;
/

-- Kiểm tra học kì có trong thời hạm 14 ngày không?
CREATE OR REPLACE FUNCTION is_within_14_days (
    hk  IN NUMBER,
    nam IN NUMBER
) RETURN BOOLEAN IS
    v_start_date DATE;
BEGIN
    IF hk = 1 THEN
        v_start_date := TO_DATE('01-09-' || nam, 'DD-MM-YYYY');
    ELSIF hk = 2 THEN
        v_start_date := TO_DATE('01-01-' || nam, 'DD-MM-YYYY');
    ELSIF hk = 3 THEN
        v_start_date := TO_DATE('01-05-' || nam, 'DD-MM-YYYY');
    ELSE
        RETURN FALSE;
    END IF;

    RETURN SYSDATE BETWEEN v_start_date AND v_start_date + 13;
END;
/


-- Kiểm tra thao tác đăng ký
CREATE OR REPLACE TRIGGER trg_check_dangky_access
BEFORE INSERT OR UPDATE OR DELETE ON DANGKY
FOR EACH ROW
DECLARE
    v_role VARCHAR2(20);
    v_hk   NUMBER;
    v_nam  NUMBER;
    v_ok   BOOLEAN := FALSE;
BEGIN
    BEGIN
        SELECT VAITRO INTO v_role FROM NHANVIEN WHERE MANLĐ = USER;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_role := 'SV';
    END;

    SELECT HK, NAM INTO v_hk, v_nam FROM MOMON WHERE MAMM = :NEW.MAMM;

    v_ok := is_within_14_days(v_hk, v_nam);

    IF (INSERTING OR UPDATING OR DELETING) AND v_role IN ('SV', 'NV PĐT') THEN
        IF NOT v_ok THEN
            RAISE_APPLICATION_ERROR(-20001, 'Chỉ được thao tác đăng ký trong 14 ngày đầu học kỳ');
        END IF;
    END IF;

    IF UPDATING THEN
        IF :OLD.DIEMTH != :NEW.DIEMTH OR :OLD.DIEMQT != :NEW.DIEMQT OR
           :OLD.DIEMCK != :NEW.DIEMCK OR :OLD.DIEMTK != :NEW.DIEMTK THEN
            IF v_role != 'NV PKT' THEN
                RAISE_APPLICATION_ERROR(-20002, 'Chỉ NV PKT mới được cập nhật điểm');
            END IF;
        END IF;
    END IF;
END;
/
*/

-- CODE TEST thao tác được
conn admin/06092004@localhost:1521/DaiHocX;
CREATE OR REPLACE FUNCTION is_within_4_months (
    hk  IN NUMBER,
    nam IN NUMBER
) RETURN BOOLEAN IS
    v_start_date DATE;
BEGIN
    IF hk = 1 THEN
        v_start_date := TO_DATE('01-09-' || nam, 'DD-MM-YYYY');
    ELSIF hk = 2 THEN
        v_start_date := TO_DATE('01-01-' || nam, 'DD-MM-YYYY');
    ELSIF hk = 3 THEN
        v_start_date := TO_DATE('01-05-' || nam, 'DD-MM-YYYY');
    ELSE
        RETURN FALSE;
    END IF;

    -- Thay 13 thành 120 ngày
    RETURN SYSDATE BETWEEN v_start_date AND ADD_MONTHS(v_start_date, 4) - 1;
END;
/

CREATE OR REPLACE TRIGGER trg_check_dangky_access
BEFORE INSERT OR UPDATE OR DELETE ON admin.ĐANGKY
FOR EACH ROW
DECLARE
    v_role VARCHAR2(20);
    v_user VARCHAR2(30) := USER;
    v_hk   NUMBER;
    v_nam  NUMBER;
    v_ok   BOOLEAN := FALSE;
BEGIN
    -- Xác định vai trò (default: SV)
    BEGIN
        SELECT VAITRO INTO v_role FROM admin.NHANVIEN WHERE MANLĐ = v_user;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_role := 'SV';
    END;

    -- Lấy học kỳ và năm của môn học (dùng NEW hoặc OLD tuỳ loại thao tác)
    SELECT HK, NAM INTO v_hk, v_nam
    FROM admin.MOMON
    WHERE MAMM = COALESCE(:NEW.MAMM, :OLD.MAMM);

    -- Kiểm tra thời gian hợp lệ
    v_ok := is_within_4_months(v_hk, v_nam);

    -- ❗ Phần INSERT / DELETE / UPDATE không phải điểm
    IF INSERTING OR DELETING OR 
       (UPDATING AND NOT (
            (:OLD.ĐIEMTH IS NOT NULL AND :NEW.ĐIEMTH != :OLD.ĐIEMTH) OR
            (:OLD.ĐIEMQT IS NOT NULL AND :NEW.ĐIEMQT != :OLD.ĐIEMQT) OR
            (:OLD.ĐIEMCK IS NOT NULL AND :NEW.ĐIEMCK != :OLD.ĐIEMCK) OR
            (:OLD.ĐIEMTK IS NOT NULL AND :NEW.ĐIEMTK != :OLD.ĐIEMTK)
        )) THEN

        -- ❌ Nếu là SV hoặc NV PĐT mà không trong 4 tháng đầu
        IF v_role IN ('SV', 'NV PĐT') AND NOT v_ok THEN
            RAISE_APPLICATION_ERROR(-20001, '❌ Chỉ được thao tác đăng ký trong 4 tháng đầu học kỳ');
        END IF;

        -- ❌ SV không được thao tác dữ liệu người khác
        IF v_role = 'SV' AND :NEW.MASV != v_user THEN
            RAISE_APPLICATION_ERROR(-20003, '❌ Sinh viên chỉ được thao tác trên dữ liệu của chính mình');
        END IF;

        -- ❌ Không được thêm dòng có điểm
        IF INSERTING THEN
            IF :NEW.ĐIEMTH IS NOT NULL OR :NEW.ĐIEMQT IS NOT NULL OR
               :NEW.ĐIEMCK IS NOT NULL OR :NEW.ĐIEMTK IS NOT NULL THEN
                RAISE_APPLICATION_ERROR(-20004, '❌ SV/NV PĐT không được thêm dữ liệu điểm');
            END IF;
        END IF;

    END IF;

    -- ❗ Cập nhật điểm
    IF UPDATING THEN
        IF (
            NVL(:NEW.ĐIEMTH, -999) != NVL(:OLD.ĐIEMTH, -999) OR
            NVL(:NEW.ĐIEMQT, -999) != NVL(:OLD.ĐIEMQT, -999) OR
            NVL(:NEW.ĐIEMCK, -999) != NVL(:OLD.ĐIEMCK, -999) OR
            NVL(:NEW.ĐIEMTK, -999) != NVL(:OLD.ĐIEMTK, -999)
        ) THEN

            IF v_role != 'NV PKT' THEN
                RAISE_APPLICATION_ERROR(-20005, '❌ Bạn không có quyền cập nhật điểm. Chỉ NV PKT được phép!');
            END IF;
        END IF;
    END IF;
END;
/
