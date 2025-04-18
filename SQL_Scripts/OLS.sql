ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;

CONN admin/06092004@localhost:1521/DaiHocX;


-- Vào container gốc
-- Ở container root (CDB$ROOT)
ALTER SESSION SET CONTAINER = CDB$ROOT;

CREATE USER C##OLS_ADMIN IDENTIFIED BY "123";
GRANT CONNECT, RESOURCE, UNLIMITED TABLESPACE TO "C##OLS_ADMIN";
GRANT SELECT ANY DICTIONARY TO "C##OLS_ADMIN";
GRANT EXECUTE ON LBACSYS.SA_COMPONENTS TO "C##OLS_ADMIN" WITH GRANT OPTION;
GRANT EXECUTE ON LBACSYS.SA_USER_ADMIN TO "C##OLS_ADMIN" WITH GRANT OPTION;
GRANT EXECUTE ON LBACSYS.SA_LABEL_ADMIN TO "C##OLS_ADMIN" WITH GRANT OPTION;
GRANT EXECUTE ON LBACSYS.SA_POLICY_ADMIN TO "C##OLS_ADMIN" WITH GRANT OPTION;
GRANT EXECUTE ON LBACSYS.TO_LBAC_DATA_LABEL TO "C##OLS_ADMIN" WITH GRANT OPTION;
GRANT EXECUTE ON LBACSYS.SA_SYSDBA TO "C##OLS_ADMIN" WITH GRANT OPTION;

GRANT LBAC_DBA TO "C##OLS_ADMIN";


GRANT INHERIT PRIVILEGES ON USER LBACSYS TO C##OLS_ADMIN CONTAINER=ALL;


CONNECT sys/123@localhost:1521/DaiHocX AS SYSDBA;

GRANT INHERIT PRIVILEGES ON USER LBACSYS TO C##OLS_ADMIN;





EXEC SA_SYSDBA.CREATE_POLICY('THONGBAO_POL', 'OLS_THONGBAO');

ALTER SESSION SET CONTAINER = CDB$ROOT;


BEGIN
  SA_SYSDBA.CREATE_POLICY(
    policy_name     => 'THONGBAO_POL',
    column_name     => 'OLS_LABEL',
    default_options => 'READ_CONTROL, WRITE_CONTROL'
  );
END;
/



