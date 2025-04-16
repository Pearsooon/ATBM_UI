-- Đặt session sang container root và tạo PDB
/*ALTER SESSION SET CONTAINER = CDB$ROOT;
CREATE PLUGGABLE DATABASE DaiHocX
ADMIN USER admin IDENTIFIED BY "06092004"
FILE_NAME_CONVERT = ('C:\app\Pearson\product\21c\oradata\XE\PDBSEED\', 
                      'C:\app\Pearson\product\21c\oradata\DaiHocX\');

ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;
*/

GRANT DBA TO admin;
GRANT EXECUTE ANY PROCEDURE TO admin;
GRANT ADMINISTER DATABASE TRIGGER TO admin;
GRANT UNLIMITED TABLESPACE TO admin;
GRANT CREATE SESSION TO admin;
GRANT SELECT ANY DICTIONARY TO admin;
GRANT SELECT ON DBA_USERS TO admin;
GRANT SELECT ON DBA_ROLES TO admin;
GRANT SELECT ON DBA_ROLE_PRIVS TO admin;
GRANT SELECT ON DBA_TAB_PRIVS TO admin;
GRANT SELECT ON DBA_COL_PRIVS TO admin;
GRANT CREATE USER TO admin;
GRANT DROP USER TO admin;
GRANT ALTER USER TO admin;
GRANT CREATE ROLE TO admin;
GRANT ALTER ANY ROLE TO admin;
GRANT DROP ANY ROLE TO admin;
GRANT GRANT ANY PRIVILEGE TO admin; -- 🟢 thêm để phân quyền sau khi tạo user
GRANT GRANT ANY ROLE TO admin;      -- (tuỳ chọn) nếu muốn cấp role cho user
GRANT SELECT ANY DICTIONARY TO admin; -- nếu muốn truy cập DBA_USERS, DBA_ROLES






CONN admin/06092004@localhost:1521/DaiHocX;

-- Các block DROP: (chú ý sắp xếp theo thứ tự phụ thuộc để tránh lỗi)
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE ĐANGKY CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE MOMON CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE HOCPHAN CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE SINHVIEN CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE NHANVIEN CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE ĐONVI CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE THONGBAO CASCADE CONSTRAINTS';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_MANLV';
EXCEPTION
  WHEN OTHERS THEN
    IF SQLCODE != -2289 THEN RAISE; END IF;
END;
/

BEGIN
  EXECUTE IMMEDIATE 'DROP SEQUENCE SEQ_MASV';
EXCEPTION
  WHEN OTHERS THEN
    IF SQLCODE != -2289 THEN RAISE; END IF;
END;
/

-- 2. TẠO LẠI SEQUENCE
CREATE SEQUENCE SEQ_MANLV START WITH 1 INCREMENT BY 1;
/

CREATE SEQUENCE SEQ_MASV START WITH 1001 INCREMENT BY 1;
/
    

-- Tạo 1 bảng theo dõi quyền cấp VIEW giả lập theo cột
CREATE TABLE GRANTED_VIEW_METADATA (
    GRANTEE        VARCHAR2(100),
    VIEW_NAME      VARCHAR2(100),
    BASE_TABLE     VARCHAR2(100),
    COLUMNS        VARCHAR2(400),
    PRIVILEGE_TYPE VARCHAR2(20),
    CREATED_AT     DATE DEFAULT SYSDATE
);


-- Tạo bảng ĐONVI (không khai báo ràng buộc FK cho TRGĐV ngay)
CREATE TABLE ĐONVI (
    MAĐV VARCHAR2(10) PRIMARY KEY,
    TENĐV VARCHAR2(100) NOT NULL,
    LOAIĐV VARCHAR2(10) CHECK (LOAIĐV IN ('Khoa', 'Phòng')),
    TRGĐV VARCHAR2(10)
);

-- Tạo bảng NHANVIEN (tham chiếu tới ĐONVI qua MAĐV)
CREATE TABLE NHANVIEN (
    MANLĐ VARCHAR2(10) PRIMARY KEY,
    HOTEN VARCHAR2(100) NOT NULL,
    PHAI VARCHAR2(10) CHECK (PHAI IN ('Nam', 'Nữ')),
    NGSINH DATE NOT NULL,
    LUONG NUMBER(10, 2) NOT NULL,
    PHUCAP NUMBER(10, 2),
    ĐT VARCHAR2(15),
    VAITRO VARCHAR2(50),
    MAĐV VARCHAR2(10) REFERENCES ĐONVI(MAĐV)
);

-- Sau khi đã tạo bảng NHANVIEN, thêm ràng buộc cho cột TRGĐV trong bảng ĐONVI
ALTER TABLE ĐONVI
    ADD CONSTRAINT fk_trgdv FOREIGN KEY (TRGĐV) REFERENCES NHANVIEN(MANLĐ);

-- Tạo bảng SINHVIEN (tham chiếu tới ĐONVI qua cột KHOA)
CREATE TABLE SINHVIEN (
    MASV VARCHAR2(10) PRIMARY KEY,
    HOTEN VARCHAR2(100) NOT NULL,
    PHAI VARCHAR2(10) CHECK (PHAI IN ('Nam', 'Nữ')),
    NGSINH DATE NOT NULL,
    ĐCHI VARCHAR2(200),
    ĐT VARCHAR2(15),
    KHOA VARCHAR2(10) REFERENCES ĐONVI(MAĐV),
    TINHTRANG VARCHAR2(50) CHECK (TINHTRANG IN ('Đang học', 'Nghỉ học', 'Bảo lưu'))
);

-- Tạo bảng HOCPHAN (tham chiếu tới ĐONVI qua MAĐV)
CREATE TABLE HOCPHAN (
    MAHP VARCHAR2(10) PRIMARY KEY,
    TENHP VARCHAR2(100) NOT NULL,
    SOTC NUMBER(3) NOT NULL,
    STLT NUMBER(3) NOT NULL,
    STTH NUMBER(3) NOT NULL,
    MAĐV VARCHAR2(10) REFERENCES ĐONVI(MAĐV)
);

-- Tạo bảng MOMON (tham chiếu tới HOCPHAN và NHANVIEN)
CREATE TABLE MOMON (
    MAMM VARCHAR2(10) PRIMARY KEY,
    MAHP VARCHAR2(10) REFERENCES HOCPHAN(MAHP),
    MAGV VARCHAR2(10) REFERENCES NHANVIEN(MANLĐ),
    HK VARCHAR2(1) CHECK (HK IN ('1', '2', '3')),
    NAM NUMBER(4) CHECK (NAM BETWEEN 1900 AND 2100)
);

-- Tạo bảng ĐANGKY (tham chiếu tới SINHVIEN và MOMON)
CREATE TABLE ĐANGKY (
    MASV VARCHAR2(10) REFERENCES SINHVIEN(MASV),
    MAMM VARCHAR2(10) REFERENCES MOMON(MAMM),
    ĐIEMTH NUMBER(5, 2),
    ĐIEMQT NUMBER(5, 2),
    ĐIEMCK NUMBER(5, 2),
    ĐIEMTK NUMBER(5, 2),
    PRIMARY KEY (MASV, MAMM)
);

CREATE TABLE THONGBAO(
    MATB VARCHAR2(10) PRIMARY KEY,
    NOIDUNG VARCHAR(100)
);

CONN admin/06092004@localhost:1521/DaiHocX;

SHOW CON_NAME;

--bảng ĐƠN VỊ---------------------------------------------------
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('CNTT', 'Công nghệ thông tin', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('HOA', 'Hóa học', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('VL', 'Vật lí', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('PDT', 'Phòng đào tạo', 'Phòng');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('CTSV', 'Phòng công tác sinh viên', 'Phòng');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('SINH', 'Sinh học', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('KTHI', 'Phòng khảo thí', 'Phòng');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('TCHC', 'tổ chức hàng chính', 'Phòng');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('TOAN', 'Toán học', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('ĐTVT', 'điện tử viển thông', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('VLIE', 'Vật liệu', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('ĐCHA', 'Địa chất', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('MTRU', 'Môi trường', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('KHLN', 'Khoa học liên ngành', 'Khoa');
INSERT INTO ĐONVI (MAĐV, TENĐV, LOAIĐV) VALUES ('QHDT', 'Quan hệ đối ngoại', 'Phòng');


--bảng NHÂN VIÊN----------------------------------------------
-- Nếu chưa có sequence, tạo sequence cho mã nhân viên
DECLARE
    -- Danh sách mã đơn vị
    TYPE t_madv IS VARRAY(15) OF VARCHAR2(10);
    v_ds_madv t_madv := t_madv('CNTT','HOA','VL','PDT','CTSV','SINH','KTHI','TCHC','TOAN', 'ĐTVT','VLIE','ĐCHA','MTRU','KHLN','QHDT');

    v_rand_madv  VARCHAR2(10);
    v_rand_days  NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để kết quả thay đổi mỗi lần chạy
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 1..500 LOOP
        -- Chọn ngẫu nhiên 1 mã đơn vị từ danh sách
        v_rand_madv := v_ds_madv(
            TRUNC(DBMS_RANDOM.VALUE(1, v_ds_madv.COUNT + 1))
        );

        -- Tạo ngày sinh ngẫu nhiên từ năm 1970 đến khoảng năm 2000
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 31 * 365)); 

        -- Sử dụng sequence để tạo mã nhân viên duy nhất với định dạng NVxxx
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');

        INSERT INTO NHANVIEN (
            MANLĐ, 
            HOTEN, 
            PHAI, 
            NGSINH, 
            LUONG, 
            PHUCAP, 
            ĐT, 
            VAITRO, 
            MAĐV
        )
        VALUES (
            v_new_manlv,
            'Nhân viên ' || i,
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            DATE '1970-01-01' + v_rand_days,
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),
            'NVCB',
            v_rand_madv
        );
    END LOOP;

    COMMIT;  -- Lưu các thay đổi
END;
/




DECLARE
    -- Danh sách mã đơn vị
    TYPE t_madv IS VARRAY(15) OF VARCHAR2(10);
    v_ds_madv t_madv := t_madv('CNTT','HOA','VL','PDT','CTSV','SINH','KTHI','TCHC','TOAN', 'ĐTVT','VLIE','ĐCHA','MTRU','KHLN','QHDT');

    v_rand_madv VARCHAR2(10);
    v_rand_days NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM, để mỗi lần chạy kết quả khác nhau
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    -- Vòng lặp 101..150
    FOR i IN 501..700 LOOP
        -- Chọn ngẫu nhiên 1 mã đơn vị từ v_ds_madv
        v_rand_madv := v_ds_madv(
            TRUNC(DBMS_RANDOM.VALUE(1, v_ds_madv.COUNT + 1))
        );

        -- Random số ngày từ 0 tới (30 * 365) ~ 30 năm
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            -- Mã nhân viên: NV101..NV150
            v_new_manlv,
            
            -- Họ tên: 'Giảng viên 101'..'Giảng viên 150'
            'Giảng viên ' || i,
            
            -- PHAI: xen kẽ Nam / Nữ
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            
            -- NGSINH: 1970-01-01 + số ngày random
            DATE '1970-01-01' + v_rand_days,
            
            -- LƯƠNG: random từ 8 triệu đến 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            
            -- PHỤ CẤP: random từ 2 triệu đến 5 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            
            -- SĐT: 10 chữ số, bắt đầu bằng 0
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),

            -- VAITRO: 'GV'
            'GV',
            
            -- Mã đơn vị random
            v_rand_madv
        );
    END LOOP;

    COMMIT; -- Xác nhận thay đổi
END;
/



DECLARE
    TYPE t_madv IS VARRAY(15) OF VARCHAR2(10);
    -- Danh sách các đơn vị sẵn có trong bảng ĐONVI
    v_ds_madv t_madv := t_madv('CNTT','HOA','VL','PDT','CTSV','SINH','KTHI','TCHC','TOAN', 'ĐTVT','VLIE','ĐCHA','MTRU','KHLN','QHDT');
    
    v_rand_days NUMBER;        -- Số ngày ngẫu nhiên
    v_new_manlv  VARCHAR2(10);  -- Mã nhân viên sinh tự động từ sequence
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để kết quả ngẫu nhiên mỗi lần chạy
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));
    
    -- Vòng lặp từ 1 đến số lượng đơn vị (8 đơn vị)
    FOR i IN 1..v_ds_madv.COUNT LOOP
        -- Sinh mã nhân viên sử dụng sequence (ví dụ: NV151, NV152, ...)
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        -- Sinh số ngày ngẫu nhiên trong khoảng 0 đến (30 * 365) ~ 30 năm
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            v_new_manlv,                      -- Mã nhân viên (từ sequence)
            'Trưởng ' || v_ds_madv(i),         -- Họ tên: “Trưởng CNTT”, “Trưởng HOA”,...
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,  -- PHAI: xen kẽ Nam/Nữ
            DATE '1970-01-01' + v_rand_days,   -- NGSINH: 1970-01-01 + số ngày ngẫu nhiên
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),       -- Lương: 8 triệu ~ 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),          -- Phụ cấp: 2 triệu ~ 5 triệu
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)), -- SĐT: 10 chữ số, bắt đầu bằng '0'
            'TRGDV',                           -- Vai trò: TRGDV
            v_ds_madv(i)                       -- Mã đơn vị tương ứng
        );
        
        -- Cập nhật bảng ĐONVI: set TRGĐV là mã nhân viên vừa tạo
        UPDATE ĐONVI
           SET TRGĐV = v_new_manlv
         WHERE MAĐV = v_ds_madv(i);
    END LOOP;
    
    COMMIT; -- Lưu thay đổi
END;
/



DECLARE
    v_rand_days NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để mỗi lần chạy ra kết quả khác nhau
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 716..735 LOOP
        -- Tạo mã nhân viên dạng: NVPDT01, NVPDT02, ...
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        -- Random số ngày trong ~30 năm (1970–2000)
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            v_new_manlv,
            'Nhân viên PĐT ' || i,
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            DATE '1970-01-01' + v_rand_days,
            -- Lương random: 8 triệu ~ 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            -- Phụ cấp random: 2 triệu ~ 5 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            -- SĐT: 10 chữ số, bắt đầu bằng '0'
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),
            'NV PĐT',
            'PDT'
        );
    END LOOP;
    
    COMMIT;  -- Xác nhận các thay đổi
END;
/


DECLARE
    v_rand_days NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để mỗi lần chạy ra kết quả khác nhau
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 736..745 LOOP
        -- Tạo mã nhân viên dạng: NVPKT01, NVPKT02, ...
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        -- Random số ngày trong ~30 năm (1970–2000)
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            v_new_manlv,
            'Nhân viên PKT ' || i,
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            DATE '1970-01-01' + v_rand_days,
            -- Lương random: 8 triệu ~ 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            -- Phụ cấp random: 2 triệu ~ 5 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            -- SĐT: 10 chữ số, bắt đầu bằng '0'
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),
            'NV PKT',    -- Vai trò
            'KTHI'       -- Mã đơn vị cho Phòng Khảo thí
        );
    END LOOP;
    
    COMMIT;  -- Xác nhận các thay đổi
END;
/


DECLARE
    v_rand_days NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để mỗi lần chạy kết quả khác nhau
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 746..755 LOOP
        -- Tạo mã nhân viên dạng: NVCTSV01, NVCTSV02, ...
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        -- Random số ngày trong ~30 năm (1970–2000)
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            v_new_manlv,
            'Nhân viên CTSV ' || i,
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            DATE '1970-01-01' + v_rand_days,
            -- Lương random: 8 triệu ~ 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            -- Phụ cấp random: 2 triệu ~ 5 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            -- SĐT: 10 chữ số, bắt đầu bằng '0'
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),
            'NV CTSV',    -- Vai trò
            'CTSV'        -- Mã đơn vị (Phòng công tác sinh viên)
        );
    END LOOP;
    
    COMMIT;  -- Xác nhận các thay đổi
END;
/


DECLARE
    v_rand_days NUMBER;
    v_new_manlv  VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM để mỗi lần chạy kết quả khác nhau
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 756..770 LOOP
        -- Tạo mã nhân viên dạng: NVCTSV01, NVCTSV02, ...
        v_new_manlv := 'NV' || LPAD(SEQ_MANLV.NEXTVAL, 3, '0');
        
        -- Random số ngày trong ~30 năm (1970–2000)
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30 * 365));
        
        INSERT INTO NHANVIEN (
            MANLĐ,
            HOTEN,
            PHAI,
            NGSINH,
            LUONG,
            PHUCAP,
            ĐT,
            VAITRO,
            MAĐV
        )
        VALUES (
            v_new_manlv,
            'Nhân viên CTSV ' || i,
            CASE WHEN MOD(i, 2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            DATE '1970-01-01' + v_rand_days,
            -- Lương random: 8 triệu ~ 15 triệu
            TRUNC(DBMS_RANDOM.VALUE(8000000, 15000000)),
            -- Phụ cấp random: 2 triệu ~ 5 triệu
            TRUNC(DBMS_RANDOM.VALUE(2000000, 5000000)),
            -- SĐT: 10 chữ số, bắt đầu bằng '0'
            '0' || TRUNC(DBMS_RANDOM.VALUE(100000000, 1000000000)),
            'NV TCHC',    -- Vai trò
            'TCHC'        -- Mã đơn vị (Phòng công tác sinh viên)
        );
    END LOOP;
    
    COMMIT;  -- Xác nhận các thay đổi
END;
/



-- bảng SINH VIÊN---------------------------------------------------------------
DECLARE
    -- Lưu danh sách mã đơn vị có loại là 'Khoa'
    TYPE t_khoa_arr IS TABLE OF VARCHAR2(10) INDEX BY PLS_INTEGER;
    v_khoa_list t_khoa_arr;
    
    v_rand_index NUMBER;
    v_rand_days  NUMBER;
    v_sdt        VARCHAR2(11);
    v_new_masv   VARCHAR2(10);
    
BEGIN
    -- Lấy tất cả MAĐV từ ĐONVI có LOAIĐV = 'Khoa'
    SELECT MAĐV
    BULK COLLECT INTO v_khoa_list
    FROM ĐONVI
    WHERE LOAIĐV = 'Khoa';

    -- Nếu không có đơn vị loại "Khoa", raise lỗi
    IF v_khoa_list.COUNT = 0 THEN
        RAISE_APPLICATION_ERROR(-20010, 'Không có đơn vị nào thuộc loại "Khoa".');
    END IF;

    -- Khởi tạo seed
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE, 'SSSSS')));

    FOR i IN 1..4000 LOOP
        -- Chọn random 1 KHOA
        v_rand_index := TRUNC(DBMS_RANDOM.VALUE(1, v_khoa_list.COUNT + 1));
        -- Sinh random ngày
        v_rand_days := TRUNC(DBMS_RANDOM.VALUE(0, 30*365));
        -- Sinh SDT
        v_sdt := '0' || LPAD(TRUNC(DBMS_RANDOM.VALUE(0,1e9)), 9, '0');
        -- Sinh mã SV
        v_new_masv := 'SV' || LPAD(TO_CHAR(SEQ_MASV.NEXTVAL), 6, '0');

        INSERT INTO SINHVIEN (
            MASV,
            HOTEN,
            PHAI,
            NGSINH,
            ĐCHI,
            ĐT,
            KHOA,
            TINHTRANG
        )
        VALUES (
            v_new_masv,
            'Sinh viên ' || i,
            CASE WHEN MOD(i,2) = 1 THEN 'Nam' ELSE 'Nữ' END,
            TRUNC(DATE '1970-01-01' + v_rand_days),
            'Địa chỉ ' || i,
            v_sdt,
            v_khoa_list(v_rand_index),
            CASE WHEN MOD(i, 3) = 0 THEN 'Bảo lưu'
                 WHEN MOD(i, 3) = 1 THEN 'Đang học'
                 ELSE 'Nghỉ học'
            END
        );
    END LOOP;

    COMMIT;
END;
/



--bảng HỌC PHẦN-----------------------------------------------------------------
DECLARE
    -- Danh sách các mã đơn vị thuộc loại “Khoa”
    TYPE t_khoa IS VARRAY(4) OF VARCHAR2(10);
    v_ds_khoa t_khoa := t_khoa('CNTT', 'HOA', 'VL', 'SINH');
    
    v_random_index NUMBER;
    v_madv         VARCHAR2(10);
BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM (để kết quả random mỗi lần)
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    FOR i IN 1..10 LOOP
        -- Chọn ngẫu nhiên 1 trong 4 khoa
        v_random_index := TRUNC(DBMS_RANDOM.VALUE(1, v_ds_khoa.COUNT + 1));
        v_madv := v_ds_khoa(v_random_index);
        
        INSERT INTO HOCPHAN (
            MAHP,
            TENHP,
            SOTC,
            STLT,
            STTH,
            MAĐV
        )
        VALUES (
            -- Mã HP: HP01..HP10
            'HP' || LPAD(i, 2, '0'),
            
            -- Tên học phần: “Học phần 1”... “Học phần 10”
            'Học phần ' || i,
            
            -- Số tín chỉ ngẫu nhiên (ví dụ 2..4)
            TRUNC(DBMS_RANDOM.VALUE(2, 5)),
            
            -- Số tiết lý thuyết ngẫu nhiên (VD 20..40)
            TRUNC(DBMS_RANDOM.VALUE(20, 41)),
            
            -- Số tiết thực hành ngẫu nhiên (VD 10..20)
            TRUNC(DBMS_RANDOM.VALUE(10, 21)),
            
            -- MAĐV là một trong 4 khoa
            v_madv
        );
    END LOOP;

    COMMIT;
END;
/



--bảng MOMON---------------------------------------------------------------------
DECLARE
    /**************************************************************
     * 1) KHAI BÁO KIỂU DỮ LIỆU VÀ CURSOR
     **************************************************************/
    -- RECORD lưu từng hàng HOCPHAN
    TYPE t_hp_rec IS RECORD (
        MAHP HOCPHAN.MAHP%TYPE,   -- Mã học phần
        MAĐV HOCPHAN.MAĐV%TYPE   -- Mã đơn vị (có chữ Đ)
    );
    -- TABLE (mảng) chứa nhiều bản ghi HOCPHAN
    TYPE t_hp_tab IS TABLE OF t_hp_rec INDEX BY PLS_INTEGER;

    -- RECORD lưu từng giảng viên
    TYPE t_gv_rec IS RECORD (
        MANLĐ NHANVIEN.MANLĐ%TYPE, -- Mã nhân viên (có chữ Đ)
        MAĐV  NHANVIEN.MAĐV%TYPE   -- Mã đơn vị (có chữ Đ)
    );
    -- TABLE (mảng) chứa nhiều giảng viên
    TYPE t_gv_tab IS TABLE OF t_gv_rec INDEX BY PLS_INTEGER;
    
    -- Con trỏ lấy tất cả HOCPHAN
    CURSOR c_hp IS
       SELECT MAHP, MAĐV
         FROM HOCPHAN;

    -- Con trỏ lấy giảng viên (VAITRO='Giảng viên')
    CURSOR c_gv IS
       SELECT MANLĐ, MAĐV
         FROM NHANVIEN
        WHERE VAITRO = 'GV';

    v_hp_list t_hp_tab;  -- mảng HOCPHAN
    v_gv_list t_gv_tab;  -- mảng giảng viên

    v_count_hp NUMBER;
    v_count_gv NUMBER;

    /**************************************************************
     * 2) BIẾN TRUNG GIAN DÙNG TRONG VÒNG LẶP
     **************************************************************/
    v_rand_gv   NUMBER;   -- chỉ số random để chọn giảng viên
    v_rand_hk   NUMBER;   -- học kỳ (1..3)
    v_rand_nam  NUMBER;   -- năm (2023..2025)
    v_mamm      VARCHAR2(10);  -- Mã môn mở

    -- Tập con HOCPHAN matching MAĐV
    v_sub_hp_list t_hp_tab;  
    v_sub_count NUMBER;

BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    /**************************************************************
     * 3) LẤY DỮ LIỆU TỪ BẢNG HOCPHAN VÀ NHANVIEN
     **************************************************************/
    OPEN c_hp;
    FETCH c_hp BULK COLLECT INTO v_hp_list;
    CLOSE c_hp;
    v_count_hp := v_hp_list.COUNT;

    OPEN c_gv;
    FETCH c_gv BULK COLLECT INTO v_gv_list;
    CLOSE c_gv;
    v_count_gv := v_gv_list.COUNT;

    -- Kiểm tra xem đã có dữ liệu chưa
    IF v_count_hp = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Bảng HOCPHAN chưa có dữ liệu.');
    END IF;
    IF v_count_gv = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Không có giảng viên (VAITRO=''Giảng viên'') trong NHANVIEN.');
    END IF;

    /**************************************************************
     * 4) VÒNG LẶP TẠO DỮ LIỆU MOMON
     **************************************************************/
    FOR i IN 1..100 LOOP
        /* 4.1) Chọn ngẫu nhiên 1 giảng viên */
        v_rand_gv := TRUNC(DBMS_RANDOM.VALUE(1, v_count_gv+1));

        -- Lưu sang các biến tạm
        DECLARE
            v_thisMANLĐ NHANVIEN.MANLĐ%TYPE;
            v_thisMAĐV  NHANVIEN.MAĐV%TYPE;
        BEGIN
            v_thisMANLĐ := v_gv_list(v_rand_gv).MANLĐ;
            v_thisMAĐV  := v_gv_list(v_rand_gv).MAĐV;

            /* 4.2) Tìm tất cả HOCPHAN có MAĐV = giảng viên này */
            v_sub_hp_list.DELETE;   -- reset mảng tạm
            v_sub_count := 0;
            
            FOR idx IN 1..v_count_hp LOOP
                IF v_hp_list(idx).MAĐV = v_thisMAĐV THEN
                    v_sub_count := v_sub_count + 1;
                    v_sub_hp_list(v_sub_count).MAHP := v_hp_list(idx).MAHP;
                    v_sub_hp_list(v_sub_count).MAĐV := v_hp_list(idx).MAĐV;
                END IF;
            END LOOP;
            
            /* Nếu không có học phần nào trùng đơn vị => bỏ qua lần này */
            IF v_sub_count = 0 THEN
                DBMS_OUTPUT.PUT_LINE('*** Không có HOCPHAN cho MAĐV=' || v_thisMAĐV ||
                                     '. Bỏ qua lần ' || i);
                CONTINUE;
            END IF;
            
            /* 4.3) Chọn ngẫu nhiên 1 HOCPHAN trong subset */
            DECLARE
                v_rand_hp_idx NUMBER;
                v_thisMAHP HOCPHAN.MAHP%TYPE;
            BEGIN
                v_rand_hp_idx := TRUNC(DBMS_RANDOM.VALUE(1, v_sub_count+1));
                v_thisMAHP := v_sub_hp_list(v_rand_hp_idx).MAHP;
                
                /* 4.4) Random HK=1..3, random năm=2023..2025 */
                v_rand_hk := TRUNC(DBMS_RANDOM.VALUE(1,4));  -- 1..3
                v_rand_nam := TRUNC(DBMS_RANDOM.VALUE(2023,2026));  -- 2023..2025
                
                /* Tạo mã môn mở (MAMM). Thay đổi tùy ý */
                v_mamm := 'MM' || LPAD(i,3,'0');  -- MM001..MM015
                
                /* 4.5) INSERT vào MOMON */
                INSERT INTO MOMON (
                    MAMM,
                    MAHP,
                    MAGV,
                    HK,
                    NAM
                )
                VALUES (
                    v_mamm,
                    v_thisMAHP,
                    v_thisMANLĐ,
                    TO_CHAR(v_rand_hk),
                    v_rand_nam
                );
            END;
        END;
    END LOOP;

    COMMIT;
END;
/


--bảng ĐĂNG KÍ-------------------------------------------------------------------
DECLARE
    /**************************************************************
     * 1) KHAI BÁO KIỂU VÀ CURSOR
     **************************************************************/
    -- Mảng MASV
    TYPE t_sv_rec IS RECORD (
        MASV SINHVIEN.MASV%TYPE
    );
    TYPE t_sv_tab IS TABLE OF t_sv_rec INDEX BY PLS_INTEGER;

    -- Mảng MAMM
    TYPE t_mm_rec IS RECORD (
        MAMM MOMON.MAMM%TYPE
    );
    TYPE t_mm_tab IS TABLE OF t_mm_rec INDEX BY PLS_INTEGER;

    CURSOR c_sv IS
        SELECT MASV FROM SINHVIEN;
    CURSOR c_mm IS
        SELECT MAMM FROM MOMON;

    v_sv_list t_sv_tab;   -- DS sinh viên
    v_mm_list t_mm_tab;   -- DS môn mở

    v_count_sv NUMBER;
    v_count_mm NUMBER;

    /**************************************************************
     * 2) BIẾN TRUNG GIAN CHO VÒNG LẶP
     **************************************************************/
    v_rand_sv   NUMBER;    -- Chỉ số random để chọn SV
    v_rand_mm   NUMBER;    -- Chỉ số random để chọn MAMM

    v_diemTH    NUMBER(5,2);
    v_diemQT    NUMBER(5,2);
    v_diemCK    NUMBER(5,2);
    v_diemTK    NUMBER(5,2);

BEGIN
    -- Khởi tạo seed cho DBMS_RANDOM
    DBMS_RANDOM.SEED(TO_NUMBER(TO_CHAR(SYSDATE,'SSSSS')));

    /**************************************************************
     * 3) LẤY DỮ LIỆU TỪ SINHVIEN VÀ MOMON
     **************************************************************/
    OPEN c_sv;
    FETCH c_sv BULK COLLECT INTO v_sv_list;
    CLOSE c_sv;
    v_count_sv := v_sv_list.COUNT;

    OPEN c_mm;
    FETCH c_mm BULK COLLECT INTO v_mm_list;
    CLOSE c_mm;
    v_count_mm := v_mm_list.COUNT;

    IF v_count_sv = 0 THEN
        RAISE_APPLICATION_ERROR(-20001, 'Bảng SINHVIEN chưa có dữ liệu.');
    END IF;
    IF v_count_mm = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Bảng MOMON chưa có dữ liệu.');
    END IF;

    /**************************************************************
     * 4) VÒNG LẶP CHÈN DỮ LIỆU VÀO ĐANGKY
     **************************************************************/
    FOR i IN 1..1000 LOOP
        -- 4.1) Chọn random 1 SV và 1 MAMM
        v_rand_sv := TRUNC(DBMS_RANDOM.VALUE(1, v_count_sv+1));
        v_rand_mm := TRUNC(DBMS_RANDOM.VALUE(1, v_count_mm+1));

        -- 4.2) Tạo ngẫu nhiên điểm (0..10)
        v_diemTH := ROUND(DBMS_RANDOM.VALUE(0, 10), 1);  -- 1 chữ số thập phân
        v_diemQT := ROUND(DBMS_RANDOM.VALUE(0, 10), 1);
        v_diemCK := ROUND(DBMS_RANDOM.VALUE(0, 10), 1);

        -- 4.3) Tính điểm tổng kết, ví dụ 20% TH + 30% QT + 50% CK
        v_diemTK := ROUND( (0.2 * v_diemTH) 
                         + (0.3 * v_diemQT)
                         + (0.5 * v_diemCK), 2);

        -- 4.4) Thử INSERT vào ĐANGKY
        BEGIN
            INSERT INTO ĐANGKY (
                MASV,
                MAMM,
                ĐIEMTH,
                ĐIEMQT,
                ĐIEMCK,
                ĐIEMTK
            )
            VALUES (
                v_sv_list(v_rand_sv).MASV,
                v_mm_list(v_rand_mm).MAMM,
                v_diemTH,
                v_diemQT,
                v_diemCK,
                v_diemTK
            );
        EXCEPTION
            WHEN DUP_VAL_ON_INDEX THEN
                -- Nếu đã tồn tại (MASV, MAMM) => bỏ qua, hoặc xử lý tùy ý
                DBMS_OUTPUT.PUT_LINE('Trùng cặp ('
                   || v_sv_list(v_rand_sv).MASV || ','
                   || v_mm_list(v_rand_mm).MAMM || ') => bỏ qua');
        END;
    END LOOP;

    COMMIT;
END;
/



/*-- Kiểm tra lại dữ liệu
conn admin/06092004@localhost:1521/DaiHocX
SELECT * FROM admin.NHANVIEN;
SELECT * FROM admin.SINHVIEN;
conn admin/06092004@localhost:1521/DaiHocX
SELECT * FROM admin.ĐONVI; 
SELECT * FROM admin.HOCPHAN;
conn admin/06092004@localhost:1521/DaiHocX
SELECT * FROM admin.MOMON;
SELECT * FROM admin.ĐANGKY;
SELECT * FROM admin.THONGBAO;
*/














