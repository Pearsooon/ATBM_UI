ALTER SESSION SET CONTAINER = CDB$ROOT;

ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;

-- NVCB.cs
-- Button select
CONN admin/06092004@localhost:1521/DaiHocX;
CREATE OR REPLACE PROCEDURE sp_select_nv_nvcb (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_NHANVIEN_NVCB;
END;
/
-- Button update
CREATE OR REPLACE PROCEDURE sp_update_phone_nvcb (
    p_sdt IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_NVCB
    SET ĐT = p_sdt;
END;
/

GRANT EXECUTE ON admin.sp_select_nv_nvcb TO NVCB;
GRANT EXECUTE ON admin.sp_update_phone_nvcb TO NVCB;



-- NV_TCHC
CONN admin/06092004@localhost:1521/DaiHocX;
-- Lấy toàn bộ nhân viên
CREATE OR REPLACE PROCEDURE sp_select_nv_tchc (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.NHANVIEN;
END;
/

-- Thêm nhân viên
CREATE OR REPLACE PROCEDURE sp_insert_nv_tchc (
    p_manld VARCHAR2,
    p_hoten VARCHAR2,
    p_phai VARCHAR2,
    p_ngsinh DATE,
    p_luong NUMBER,
    p_phucap NUMBER,
    p_dt VARCHAR2,
    p_vaitro VARCHAR2,
    p_madv VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    INSERT INTO admin.NHANVIEN (MANLĐ, HOTEN, PHAI, NGSINH, LUONG, PHUCAP, ĐT, VAITRO, MAĐV)
    VALUES (p_manld, p_hoten, p_phai, p_ngsinh, p_luong, p_phucap, p_dt, p_vaitro, p_madv);
END;
/

-- Cập nhật thông tin nhân viên
CREATE OR REPLACE PROCEDURE sp_update_nv_tchc (
    p_manld VARCHAR2,
    p_hoten VARCHAR2,
    p_phai VARCHAR2,
    p_ngsinh DATE,
    p_luong NUMBER,
    p_phucap NUMBER,
    p_dt VARCHAR2,
    p_vaitro VARCHAR2,
    p_madv VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.NHANVIEN
    SET HOTEN = p_hoten,
        PHAI = p_phai,
        NGSINH = p_ngsinh,
        LUONG = p_luong,
        PHUCAP = p_phucap,
        ĐT = p_dt,
        VAITRO = p_vaitro,
        MAĐV = p_madv
    WHERE MANLĐ = p_manld;
END;
/

-- Xóa nhân viên
CREATE OR REPLACE PROCEDURE sp_delete_nv_tchc (
    p_manld VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    DELETE FROM admin.NHANVIEN WHERE MANLĐ = p_manld;
END;
/

GRANT EXECUTE ON admin.sp_update_nv_tchc TO NV_TCHC;
GRANT EXECUTE ON admin.sp_delete_nv_tchc TO NV_TCHC;
GRANT EXECUTE ON admin.sp_select_nv_tchc TO NV_TCHC;
GRANT EXECUTE ON admin.sp_insert_nv_tchc TO NV_TCHC;



--TRGDV.cs
conn admin/06092004@localhost:1521/DaiHocX;
-- Tìm nhân viên mình quản lý
CREATE OR REPLACE PROCEDURE SP_SELECT_NV_TRGDV(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.V_NHANVIEN_TRGDV;
END;
/
-- Update ĐT của bản thân
CREATE OR REPLACE PROCEDURE sp_update_phone_trgdv (
    p_sdt IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_TRGDV
    SET ĐT = p_sdt
    WHERE MANLĐ = SYS_CONTEXT('USERENV', 'SESSION_USER');
END;
/

-- Xem thông tin giảng dạy của Nhân viên mình quản lí
CREATE OR REPLACE PROCEDURE SP_SELECT_MOMON_TRGDV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_MOMON_TRGDV;
END;
/

-- Cấp quyền cho TRGĐV gọi thủ tục
GRANT EXECUTE ON admin.SP_SELECT_MOMON_TRGDV TO TRGĐV;
GRANT EXECUTE ON admin.SP_SELECT_NV_TRGDV TO TRGĐV;
GRANT EXECUTE ON admin.sp_update_phone_trgdv TO TRGĐV;


-- GV
conn admin/06092004@localhost:1521/DaiHocX;
-- Xem thông tin cá nhân
CREATE OR REPLACE PROCEDURE SP_SELECT_NV_GV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_NHANVIEN_GV;
END;
/

-- Update SĐT của bản thân
CREATE OR REPLACE PROCEDURE sp_update_phone_gv (
    p_sdt IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_GV
    SET ĐT = p_sdt
    WHERE MANLĐ = SYS_CONTEXT('USERENV', 'SESSION_USER');
END;
/

GRANT EXECUTE ON SP_SELECT_NV_GV TO GV;
GRANT EXECUTE ON sp_update_phone_gv TO GV;

-- Xem phân công giảng dạy
CREATE OR REPLACE PROCEDURE SP_SELECT_MOMON_GV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_MOMON_GV;
END;
/

GRANT EXECUTE ON SP_SELECT_MOMON_GV TO GV;

-- Xem sinh viên thuộc khoa
CREATE OR REPLACE PROCEDURE SP_SELECT_SV_GV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.SINHVIEN;
END;
/

GRANT EXECUTE ON SP_SELECT_SV_GV TO GV;



-- Xem bảng điểm các lớp học phần phụ trách
CREATE OR REPLACE PROCEDURE SP_SELECT_DANGKY_GV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.ĐANGKY;
END;
/

GRANT EXECUTE ON SP_SELECT_DANGKY_GV TO GV;



-- NV_CTSV
conn admin/06092004@localhost:1521/DaiHocX;
-- Xem thông tin cá nhân
CREATE OR REPLACE PROCEDURE SP_SELECT_NV_CTSV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_NHANVIEN_CTSV;
END;
/

GRANT EXECUTE ON SP_SELECT_NV_CTSV TO NV_CTSV;

-- Cập nhật số điện thoại
CREATE OR REPLACE PROCEDURE SP_UPDATE_PHONE_CTSV (
    p_sdt IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_CTSV
    SET ĐT = p_sdt
    WHERE MANLĐ = SYS_CONTEXT('USERENV', 'SESSION_USER');
END;
/

GRANT EXECUTE ON SP_UPDATE_PHONE_CTSV TO NV_CTSV;


-- Xem SV
CREATE OR REPLACE PROCEDURE SP_SELECT_SINHVIEN_CTSV (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.SINHVIEN;
END;
/

-- INSERT
CREATE OR REPLACE PROCEDURE SP_INSERT_SINHVIEN_CTSV (
    p_masv     IN admin.SINHVIEN.MASV%TYPE,
    p_hoten    IN admin.SINHVIEN.HOTEN%TYPE,
    p_phai     IN admin.SINHVIEN.PHAI%TYPE,
    p_ngsinh   IN admin.SINHVIEN.NGSINH%TYPE,
    p_dchi     IN admin.SINHVIEN.ĐCHI%TYPE,
    p_dt       IN admin.SINHVIEN.ĐT%TYPE,
    p_khoa     IN admin.SINHVIEN.KHOA%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    INSERT INTO admin.SINHVIEN (
        MASV, HOTEN, PHAI, NGSINH, ĐCHI, ĐT, KHOA, TINHTRANG
    ) VALUES (
        p_masv, p_hoten, p_phai, p_ngsinh, p_dchi, p_dt, p_khoa, NULL
    );
END;
/

-- UPDATE
CREATE OR REPLACE PROCEDURE SP_UPDATE_SINHVIEN_CTSV (
    p_masv       IN admin.SINHVIEN.MASV%TYPE,
    p_hoten      IN admin.SINHVIEN.HOTEN%TYPE,
    p_phai       IN admin.SINHVIEN.PHAI%TYPE,
    p_ngsinh     IN admin.SINHVIEN.NGSINH%TYPE,
    p_dchi       IN admin.SINHVIEN.ĐCHI%TYPE,
    p_dt         IN admin.SINHVIEN.ĐT%TYPE,
    p_khoa       IN admin.SINHVIEN.KHOA%TYPE,
    p_tinhtrang  IN admin.SINHVIEN.TINHTRANG%TYPE
)
AUTHID CURRENT_USER
IS
    v_old_tinhtrang admin.SINHVIEN.TINHTRANG%TYPE;
BEGIN
    -- Lấy tình trạng hiện tại
    SELECT TINHTRANG INTO v_old_tinhtrang
    FROM admin.SINHVIEN
    WHERE MASV = p_masv;

    -- Nếu có sự thay đổi TINHTRANG → báo lỗi
    IF v_old_tinhtrang IS NULL AND p_tinhtrang IS NOT NULL THEN
        RAISE_APPLICATION_ERROR(-20009, 'Bạn không được phép cập nhật tình trạng học vụ');
    ELSIF v_old_tinhtrang IS NOT NULL AND v_old_tinhtrang != p_tinhtrang THEN
        RAISE_APPLICATION_ERROR(-20009, 'Bạn không được phép cập nhật tình trạng học vụ');
    END IF;

    -- Cho phép update các trường còn lại
    UPDATE admin.SINHVIEN
    SET
        HOTEN   = p_hoten,
        PHAI    = p_phai,
        NGSINH  = p_ngsinh,
        ĐCHI    = p_dchi,
        ĐT      = p_dt,
        KHOA    = p_khoa
    WHERE MASV = p_masv;
END;
/


-- DELETE
CREATE OR REPLACE PROCEDURE SP_DELETE_SINHVIEN_CTSV (
    p_masv IN admin.SINHVIEN.MASV%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    DELETE FROM admin.SINHVIEN
    WHERE MASV = p_masv;
END;
/

GRANT EXECUTE ON SP_SELECT_SINHVIEN_CTSV TO NV_CTSV;
GRANT EXECUTE ON SP_INSERT_SINHVIEN_CTSV TO NV_CTSV;
GRANT EXECUTE ON SP_UPDATE_SINHVIEN_CTSV TO NV_CTSV;
GRANT EXECUTE ON SP_DELETE_SINHVIEN_CTSV TO NV_CTSV;



-- NV PĐT
conn admin/06092004@localhost:1521/DaiHocX;
-- Xem và cập nhật thông tin SĐT
CREATE OR REPLACE PROCEDURE SP_SELECT_NV_PDT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_NHANVIEN_PDT;
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_PHONE_PDT(p_sdt IN VARCHAR2)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_PDT
    SET ĐT = p_sdt
    WHERE MANLĐ = SYS_CONTEXT('USERENV', 'SESSION_USER');
END;
/

GRANT EXECUTE ON SP_SELECT_NV_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_UPDATE_PHONE_PDT TO NV_PĐT;

-- Quản lý MOMON (HK hiện tại)
CREATE OR REPLACE PROCEDURE SP_SELECT_MOMON_PDT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.V_MOMON_PDT;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERT_MOMON_PDT (
    p_mamm IN VARCHAR2, p_mahp IN VARCHAR2, p_magv IN VARCHAR2,
    p_hk IN VARCHAR2, p_nam IN NUMBER
)
AUTHID CURRENT_USER
IS
BEGIN
    INSERT INTO admin.V_MOMON_PDT VALUES (p_mamm, p_mahp, p_magv, p_hk, p_nam);
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_MOMON_PDT (
    p_mamm IN VARCHAR2, p_mahp IN VARCHAR2, p_magv IN VARCHAR2,
    p_hk IN VARCHAR2, p_nam IN NUMBER
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_MOMON_PDT
    SET MAHP = p_mahp, MAGV = p_magv, HK = p_hk, NAM = p_nam
    WHERE MAMM = p_mamm;
END;
/

CREATE OR REPLACE PROCEDURE SP_DELETE_MOMON_PDT(p_mamm IN VARCHAR2)
AUTHID CURRENT_USER
IS
BEGIN
    DELETE FROM admin.V_MOMON_PDT WHERE MAMM = p_mamm;
END;
/

GRANT SELECT ON admin.MOMON TO NV_PĐT;
GRANT EXECUTE ON SP_SELECT_MOMON_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_INSERT_MOMON_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_UPDATE_MOMON_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_DELETE_MOMON_PDT TO NV_PĐT;


-- Xem SV và Cập nhật cột TINHTRANG của SINHVIEN
CREATE OR REPLACE PROCEDURE SP_SELECT_SINHVIEN_PDT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.SINHVIEN;
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_TINHTRANG (
    p_masv IN VARCHAR2,
    p_tinhtrang IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.SINHVIEN
    SET TINHTRANG = p_tinhtrang
    WHERE MASV = p_masv;
END;
/

GRANT EXECUTE ON SP_SELECT_SINHVIEN_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_UPDATE_TINHTRANG TO NV_PĐT;


-- Xem, T,X,S bảng đăng ký 
CREATE OR REPLACE PROCEDURE SP_SELECT_DANGKY_PDT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.ĐANGKY;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERT_DANGKY_PDT(
    p_masv   IN admin.ĐANGKY.MASV%TYPE,
    p_mamm   IN admin.ĐANGKY.MAMM%TYPE,
    p_diemth IN admin.ĐANGKY.ĐIEMTH%TYPE,
    p_diemqt IN admin.ĐANGKY.ĐIEMQT%TYPE,
    p_diemck IN admin.ĐANGKY.ĐIEMCK%TYPE,
    p_diemtk IN admin.ĐANGKY.ĐIEMTK%TYPE
)
AUTHID DEFINER
IS
BEGIN
    INSERT INTO admin.ĐANGKY(MASV, MAMM, ĐIEMTH, ĐIEMQT, ĐIEMCK, ĐIEMTK)
    VALUES (p_masv, p_mamm, p_diemth, p_diemqt, p_diemck, p_diemtk);
END;
/

CREATE OR REPLACE PROCEDURE SP_DELETE_DANGKY_PDT(
    p_masv IN VARCHAR2, p_mamm IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    DELETE FROM admin.ĐANGKY
    WHERE MASV = p_masv AND MAMM = p_mamm;
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_DANGKY_PDT (
    p_masv     IN admin.ĐANGKY.MASV%TYPE,
    p_mamm     IN admin.ĐANGKY.MAMM%TYPE,
    p_diemth   IN admin.ĐANGKY.ĐIEMTH%TYPE,
    p_diemqt   IN admin.ĐANGKY.ĐIEMQT%TYPE,
    p_diemck   IN admin.ĐANGKY.ĐIEMCK%TYPE,
    p_diemtk   IN admin.ĐANGKY.ĐIEMTK%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.ĐANGKY
    SET 
        MASV = p_masv,
        MAMM = p_mamm,
        ĐIEMTH = p_diemth,
        ĐIEMQT = p_diemqt,
        ĐIEMCK = p_diemck,
        ĐIEMTK = p_diemtk
    WHERE MASV = p_masv AND MAMM = p_mamm;
END;
/

GRANT SELECT ON admin.ĐANGKY TO NV_PĐT;
GRANT UPDATE ON admin.ĐANGKY TO NV_PĐT;
GRANT EXECUTE ON SP_UPDATE_DANGKY_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_SELECT_DANGKY_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_INSERT_DANGKY_PDT TO NV_PĐT;
GRANT EXECUTE ON SP_DELETE_DANGKY_PDT TO NV_PĐT;




-- SINHVIEN
conn admin/06092004@localhost:1521/DaiHocX;
-- Xem môn mở theo khoa của sinh viên
CREATE OR REPLACE PROCEDURE SP_SELECT_MOMON_SV(
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.V_MOMON_SV;
END;
/


-- Xem thông tin chính mình
CREATE OR REPLACE PROCEDURE SP_SELECT_SV_SELF(
    p_masv   IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.SINHVIEN WHERE MASV = p_masv;
END;
/


-- Cập nhật địa chỉ và số điện thoại
CREATE OR REPLACE PROCEDURE SP_UPDATE_SV_SELF(
    p_masv IN VARCHAR2,
    p_dchi IN VARCHAR2,
    p_dt   IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.SINHVIEN
    SET ĐCHI = p_dchi,
        ĐT = p_dt
    WHERE MASV = p_masv;
END;
/

-- Xem danh sách đăng ký của chính sinh viên
CREATE OR REPLACE PROCEDURE SP_SELECT_DANGKY_SV(
    p_masv   IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.ĐANGKY WHERE MASV = p_masv;
END;
/

-- Insert đăng ký
CREATE OR REPLACE PROCEDURE SP_INSERT_DANGKY_SV(
    p_masv   IN admin.ĐANGKY.MASV%TYPE,
    p_mamm   IN admin.ĐANGKY.MAMM%TYPE,
    p_diemth IN admin.ĐANGKY.ĐIEMTH%TYPE,
    p_diemqt IN admin.ĐANGKY.ĐIEMQT%TYPE,
    p_diemck IN admin.ĐANGKY.ĐIEMCK%TYPE,
    p_diemtk IN admin.ĐANGKY.ĐIEMTK%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    INSERT INTO admin.ĐANGKY(MASV, MAMM, ĐIEMTH, ĐIEMQT, ĐIEMCK, ĐIEMTK)
    VALUES (p_masv, p_mamm, p_diemth, p_diemqt, p_diemck, p_diemtk);
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_DANGKY_SV (
    p_masv     IN admin.ĐANGKY.MASV%TYPE,
    p_mamm     IN admin.ĐANGKY.MAMM%TYPE,
    p_diemth   IN admin.ĐANGKY.ĐIEMTH%TYPE,
    p_diemqt   IN admin.ĐANGKY.ĐIEMQT%TYPE,
    p_diemck   IN admin.ĐANGKY.ĐIEMCK%TYPE,
    p_diemtk   IN admin.ĐANGKY.ĐIEMTK%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.ĐANGKY
    SET 
        MASV = p_masv,
        MAMM = p_mamm,
        ĐIEMTH = p_diemth,
        ĐIEMQT = p_diemqt,
        ĐIEMCK = p_diemck,
        ĐIEMTK = p_diemtk
    WHERE MASV = p_masv AND MAMM = p_mamm;
END;
/

-- Xoá đăng ký
CREATE OR REPLACE PROCEDURE SP_DELETE_DANGKY_SV(
    p_masv IN VARCHAR2,
    p_mamm IN VARCHAR2
)
AUTHID CURRENT_USER
IS
BEGIN
    DELETE FROM admin.ĐANGKY WHERE MASV = p_masv AND MAMM = p_mamm;
END;
/

GRANT EXECUTE ON SP_SELECT_MOMON_SV TO SV;
GRANT EXECUTE ON SP_SELECT_SV_SELF TO SV;
GRANT EXECUTE ON SP_UPDATE_SV_SELF TO SV;
GRANT EXECUTE ON SP_SELECT_DANGKY_SV TO SV;
GRANT EXECUTE ON SP_INSERT_DANGKY_SV TO SV;
GRANT EXECUTE ON SP_UPDATE_DANGKY_SV TO SV;
GRANT EXECUTE ON SP_DELETE_DANGKY_SV TO SV;

GRANT SELECT ON admin.SINHVIEN TO SV;
GRANT UPDATE (ĐCHI, ĐT) ON admin.SINHVIEN TO SV;

GRANT SELECT, INSERT, UPDATE, DELETE ON admin.ĐANGKY TO SV;




--NV PKT
conn admin/06092004@localhost:1521/DaiHocX;
-- Xem và cập nhật SĐT
CREATE OR REPLACE PROCEDURE SP_SELECT_NV_PKT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM admin.V_NHANVIEN_PKT;
END;
/

CREATE OR REPLACE PROCEDURE SP_UPDATE_PHONE_PKT(p_sdt IN VARCHAR2)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.V_NHANVIEN_PKT
    SET ĐT = p_sdt
    WHERE MANLĐ = SYS_CONTEXT('USERENV', 'SESSION_USER');
END;
/


-- Xem và Cập nhật điểm
CREATE OR REPLACE PROCEDURE SP_SELECT_DANGKY_PKT(p_cursor OUT SYS_REFCURSOR)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM admin.ĐANGKY;
END;
/

CREATE OR REPLACE PROCEDURE SP_PKT_UPDATE_DIEM (
    p_masv     IN admin.ĐANGKY.MASV%TYPE,
    p_mamm     IN admin.ĐANGKY.MAMM%TYPE,
    p_diemth   IN admin.ĐANGKY.ĐIEMTH%TYPE,
    p_diemqt   IN admin.ĐANGKY.ĐIEMQT%TYPE,
    p_diemck   IN admin.ĐANGKY.ĐIEMCK%TYPE,
    p_diemtk   IN admin.ĐANGKY.ĐIEMTK%TYPE
)
AUTHID CURRENT_USER
IS
BEGIN
    UPDATE admin.ĐANGKY
    SET ĐIEMTH = p_diemth,
        ĐIEMQT = p_diemqt,
        ĐIEMCK = p_diemck,
        ĐIEMTK = p_diemtk
    WHERE MASV = p_masv AND MAMM = p_mamm;
END;
/

-- Truy cập và sửa thông tin cá nhân
GRANT EXECUTE ON admin.SP_SELECT_NV_PKT TO NV_PKT;
GRANT EXECUTE ON admin.SP_UPDATE_PHONE_PKT TO NV_PKT;

-- Cập nhật điểm
GRANT SELECT, UPDATE (ĐIEMTH, ĐIEMQT, ĐIEMCK, ĐIEMTK) ON admin.ĐANGKY TO NV_PKT;
GRANT EXECUTE ON admin.SP_PKT_UPDATE_DIEM TO NV_PKT;
GRANT EXECUTE ON admin.SP_SELECT_DANGKY_PKT TO NV_PKT;


