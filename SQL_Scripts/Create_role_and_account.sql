ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;

CONN admin/06092004@localhost:1521/DaiHocX;

-- Các vai trò người dùng
CREATE ROLE GV;        -- Giảng viên
CREATE ROLE TRGĐV;     -- Trưởng đơn vị
CREATE ROLE NV_PĐT;    -- Nhân viên Phòng Đào tạo
CREATE ROLE NV_PKT;    -- Nhân viên Phòng Khảo thí
CREATE ROLE NV_TCHC;   -- Nhân viên Phòng Tổ chức - Hành chính
CREATE ROLE NV_CTSV;   -- Nhân viên Công tác sinh viên
CREATE ROLE NVCB;      -- Nhân viên cơ bản
CREATE ROLE SV;        -- Sinh viên

-- Tạo tài khoản cho NHANVIEN
BEGIN
  FOR r IN (SELECT MANLĐ FROM NHANVIEN) LOOP
    BEGIN
      EXECUTE IMMEDIATE 'CREATE USER ' || r.MANLĐ || 
                        ' IDENTIFIED BY "' || r.MANLĐ || '"';
      EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || r.MANLĐ;
    EXCEPTION
      WHEN OTHERS THEN
        IF SQLCODE != -01920 THEN -- bỏ qua lỗi "user đã tồn tại"
          RAISE;
        END IF;
    END;
  END LOOP;
END;
/

-- Tạo tài khoản cho SINHVIEN
BEGIN
  FOR r IN (SELECT MASV FROM SINHVIEN) LOOP
    BEGIN
      EXECUTE IMMEDIATE 'CREATE USER ' || r.MASV || 
                        ' IDENTIFIED BY "' || r.MASV || '"';
      EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || r.MASV;
    EXCEPTION
      WHEN OTHERS THEN
        IF SQLCODE != -01920 THEN -- bỏ qua lỗi "user đã tồn tại"
          RAISE;
        END IF;
    END;
  END LOOP;
END;
/


-- Gán role cho từng user
BEGIN
  FOR r IN (SELECT MANLĐ, VAITRO FROM NHANVIEN) LOOP
    BEGIN
      IF r.VAITRO = 'GV' THEN
        EXECUTE IMMEDIATE 'GRANT GV TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'TRGDV' THEN
        EXECUTE IMMEDIATE 'GRANT TRGĐV TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'NV PĐT' THEN
        EXECUTE IMMEDIATE 'GRANT NV_PĐT TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'NV PKT' THEN
        EXECUTE IMMEDIATE 'GRANT NV_PKT TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'NV TCHC' THEN
        EXECUTE IMMEDIATE 'GRANT NV_TCHC TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'NV CTSV' THEN
        EXECUTE IMMEDIATE 'GRANT NV_CTSV TO ' || r.MANLĐ;
      ELSIF r.VAITRO = 'NVCB' THEN
        EXECUTE IMMEDIATE 'GRANT NVCB TO ' || r.MANLĐ;
      END IF;
    EXCEPTION
      WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Lỗi khi gán role cho: ' || r.MANLĐ || ' - ' || SQLERRM);
    END;
  END LOOP;
END;
/

BEGIN
  FOR r IN (SELECT MASV FROM SINHVIEN) LOOP
    BEGIN
      EXECUTE IMMEDIATE 'GRANT SV TO ' || r.MASV;
    EXCEPTION
      WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Lỗi khi gán role SV cho: ' || r.MASV || ' - ' || SQLERRM);
    END;
  END LOOP;
END;
/
