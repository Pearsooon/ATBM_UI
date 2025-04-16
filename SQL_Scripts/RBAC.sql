ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;

CONN admin/06092004@localhost:1521/DaiHocX;

--yêu cầu 1, phân hệ 2.
--vai trò: NVCB 
CREATE OR REPLACE VIEW V_NHANVIEN_NVCB AS
SELECT *
FROM   NHANVIEN
WHERE  MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER');

-- Cấp quyền
GRANT SELECT, UPDATE(ĐT) ON V_NHANVIEN_NVCB TO NVCB;

--vai trò: TRGDV
CREATE OR REPLACE VIEW V_NHANVIEN_TRGDV AS
SELECT MANLĐ,
       HOTEN,
       PHAI,
       NGSINH,
       /* Hiện lương & phụ cấp của chính TRGĐV, che (NULL) của người khác */
       CASE WHEN MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER')
            THEN LUONG END      AS LUONG,
       CASE WHEN MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER')
            THEN PHUCAP END     AS PHUCAP,
       ĐT,
       VAITRO,
       MAĐV
FROM   NHANVIEN
WHERE  /* 1) nhân viên thuộc đơn vị mình  OR  2) chính bản thân mình            */
       MAĐV IN (SELECT MAĐV
                  FROM ĐONVI
                 WHERE TRGĐV = SYS_CONTEXT('USERENV','SESSION_USER'))
   OR  MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER')
WITH  CHECK OPTION CONSTRAINT trg_view_chk;


-- Cấp quyền
GRANT SELECT, UPDATE(ĐT) ON admin.V_NHANVIEN_TRGDV TO TRGĐV;




--vai tro: NV TCHC
GRANT SELECT, INSERT, UPDATE, DELETE ON NHANVIEN TO NV_TCHC;


--vai trò: giảng viên
CREATE OR REPLACE VIEW V_NHANVIEN_GV AS
SELECT *
FROM NHANVIEN 
WHERE MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT, UPDATE(ĐT) ON V_NHANVIEN_GV TO GV;


-- VAI TRÒ: NHÂN VIÊN PHÒNG ĐÀO TẠO
CREATE OR REPLACE VIEW V_NHANVIEN_PDT AS
SELECT * 
FROM NHANVIEN
WHERE MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT, UPDATE(ĐT) ON V_NHANVIEN_PDT TO NV_PĐT;


--VAI TRÒ: NHÂN VIÊN PHÒNG KHẢO THÍ:
CREATE OR REPLACE VIEW V_NHANVIEN_PKT AS
SELECT *
FROM NHANVIEN
WHERE MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT, UPDATE(ĐT) ON V_NHANVIEN_PKT TO NV_PKT;


--VAI TRÒ: NHÂN VIÊN CÔNG TÁC SINH VIÊN
CREATE OR REPLACE VIEW V_NHANVIEN_CTSV AS
SELECT *
FROM NHANVIEN
WHERE MANLĐ = SYS_CONTEXT('USERENV','SESSION_USER');

GRANT SELECT, UPDATE(ĐT) ON V_NHANVIEN_CTSV TO NV_CTSV;


-------------------------------------------------------------------------
--bảng MOMON

--quyền giảng viên.
CREATE OR REPLACE VIEW V_MOMON_GV AS
SELECT *
FROM   MOMON
WHERE  MAGV = SYS_CONTEXT('USERENV','SESSION_USER')   -- lọc đúng mã GV
WITH  CHECK OPTION  CONSTRAINT gv_momon_chk;   -- bảo vệ khi UPDATE
     
/* 3. Cấp quyền SELECT cho vai trò GV */
GRANT SELECT ON v_momon_gv TO gv;


--quyền của nhân viên phòng đào tạo
CREATE OR REPLACE FUNCTION f_hk_now
RETURN VARCHAR2
AS
    v_hk  VARCHAR2(1);
BEGIN
    CASE
      WHEN EXTRACT(MONTH FROM SYSDATE) BETWEEN 9 AND 12 THEN v_hk := '1';
      WHEN EXTRACT(MONTH FROM SYSDATE) BETWEEN 1 AND 4 THEN v_hk := '2';
      ELSE v_hk := '3';  -- hè
    END CASE;
    RETURN v_hk;
END;
/

CREATE OR REPLACE FUNCTION f_nam_now
RETURN NUMBER
AS
BEGIN
    RETURN EXTRACT(YEAR FROM SYSDATE);
END;
/


CREATE OR REPLACE VIEW V_MOMON_PDT AS
SELECT *
FROM   MOMON
WHERE  hk  = f_hk_now()
  AND  nam = f_nam_now()
WITH  CHECK OPTION  CONSTRAINT v_momon_pdt_chk;            -- chặn INSERT/UPDATE lệch học kỳ‑năm

GRANT SELECT, INSERT, UPDATE, DELETE ON V_MOMON_PDT TO NV_PĐT;


--quyền của trưởng đơn vị:
CREATE OR REPLACE VIEW V_MOMON_TRGDV AS
SELECT m.*
FROM   MOMON        m
JOIN   NHANVIEN     nv  ON nv.manlđ = m.magv      -- tìm đơn vị của giảng viên
WHERE  nv.mađv IN ( SELECT mađv
                     FROM   đonvi
                     WHERE  trgđv = SYS_CONTEXT('USERENV','SESSION_USER') );


      
GRANT SELECT ON v_momon_trgdv TO TRGĐV;


--quyền cho sinh viên:
CREATE OR REPLACE VIEW V_MOMON_SV AS
SELECT m.*
FROM   MOMON     m
JOIN   HOCPHAN   h   ON h.MAHP = m.MAHP          -- tìm đơn vị phụ trách HP
WHERE  h.mađv = ( SELECT s.khoa                  -- Khoa của SV đang đăng nhập
                  FROM   SINHVIEN s
                  WHERE  s.MASV = SYS_CONTEXT('USERENV','SESSION_USER') )
WITH  READ ONLY;                                 -- SV chỉ được xem


GRANT SELECT ON V_MOMON_SV TO SV;







