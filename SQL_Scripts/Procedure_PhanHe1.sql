ALTER SESSION SET CONTAINER = CDB$ROOT;
ALTER PLUGGABLE DATABASE DaiHocX OPEN;
ALTER SESSION SET CONTAINER = DaiHocX;
SHOW CON_NAME;


--I. Proc Quán lí user/role
CONN admin/06092004@localhost:1521/DaiHocX;


--1. Tạo user
CREATE OR REPLACE PROCEDURE sp_create_user (
    p_username IN VARCHAR2,
    p_password IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'CREATE USER ' || p_username || ' IDENTIFIED BY ' || p_password;
    
    -- Cấp quyền đăng nhập
    EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || p_username;
END;
/

--2. Xoá user
CREATE OR REPLACE PROCEDURE sp_delete_user (
    p_username IN VARCHAR2
)
IS
BEGIN
    -- Xóa user
    EXECUTE IMMEDIATE 'DROP USER ' || p_username || ' CASCADE';
END;
/

--3. Update user (password)
CREATE OR REPLACE PROCEDURE sp_alter_user (
    p_username     IN VARCHAR2,
    p_new_password IN VARCHAR2
)
IS
BEGIN
    EXECUTE IMMEDIATE 'ALTER USER ' || p_username || ' IDENTIFIED BY ' || p_new_password;
END;
/

-- 4. Tìm kiếm user nhất định
CREATE OR REPLACE PROCEDURE sp_find_user (
    p_username IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT USERNAME, USER_ID, CREATED
        FROM DBA_USERS
        WHERE USERNAME LIKE '%' || UPPER(p_username) || '%';
END;
/


--5. Tạo role
CREATE OR REPLACE PROCEDURE sp_create_role (
    p_rolename IN VARCHAR2,
    p_password IN VARCHAR2
)
IS
BEGIN
    IF p_password IS NULL THEN
        EXECUTE IMMEDIATE 'CREATE ROLE ' || p_rolename;
    ELSE
        EXECUTE IMMEDIATE 'CREATE ROLE ' || p_rolename || ' IDENTIFIED BY ' || p_password;
    END IF;
END;
/

--6. Xoá role
CREATE OR REPLACE PROCEDURE sp_delete_role (
    p_rolename IN VARCHAR2
)
IS
BEGIN
    -- Thu hồi role khỏi tất cả user trước
    FOR rec IN (SELECT GRANTEE FROM DBA_ROLE_PRIVS WHERE GRANTED_ROLE = UPPER(p_rolename)) LOOP
        EXECUTE IMMEDIATE 'REVOKE ' || p_rolename || ' FROM ' || rec.GRANTEE;
    END LOOP;
    
    -- Xoá
    EXECUTE IMMEDIATE 'DROP ROLE ' || p_rolename;
END;
/


--7. Update role
CREATE OR REPLACE PROCEDURE sp_update_role_password (
    p_rolename IN VARCHAR2,
    p_password IN VARCHAR2
)
IS  
BEGIN
    IF p_password IS NULL THEN
        EXECUTE IMMEDIATE 'ALTER ROLE ' || p_rolename || ' NOT IDENTIFIED';
    ELSE
        EXECUTE IMMEDIATE 'ALTER ROLE ' || p_rolename || ' IDENTIFIED BY ' || p_password;
    END IF;
END;
/


--8. Tìm kiếm role
CREATE OR REPLACE PROCEDURE sp_find_role (
    p_rolename IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT ROLE, ROLE_ID, PASSWORD_REQUIRED
        FROM DBA_ROLES
        WHERE ROLE LIKE '%' || UPPER(p_rolename) || '%';
END;
/


CONN admin/06092004@localhost:1521/DaiHocX;
-- II. Cấp quyền cho User/Role
-- PhanHe1_GrantRevokeUserRole
-- Tìm grantee
CREATE OR REPLACE PROCEDURE sp_find_grantee_table_privs (
    p_grantee IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM DBA_TAB_PRIVS WHERE GRANTEE = UPPER(p_grantee);

END;
/

-- Tìm grantee cột
CREATE OR REPLACE FUNCTION split_columns(p_str IN VARCHAR2)
  RETURN SYS.ODCIVARCHAR2LIST
AS
  v_list SYS.ODCIVARCHAR2LIST := SYS.ODCIVARCHAR2LIST();
  v_pos  PLS_INTEGER := 1;
  v_idx  PLS_INTEGER := 1;
  v_len  PLS_INTEGER;
BEGIN
  v_len := LENGTH(p_str);
  WHILE v_pos <= v_len LOOP
    -- lấy vị trí dấu phẩy tiếp theo
    DECLARE
      v_next NUMBER := INSTR(p_str, ',', v_pos);
    BEGIN
      IF v_next = 0 THEN
        v_list.EXTEND;
        v_list(v_idx) := TRIM(SUBSTR(p_str, v_pos));
        EXIT;
      ELSE
        v_list.EXTEND;
        v_list(v_idx) := TRIM(SUBSTR(p_str, v_pos, v_next - v_pos));
        v_pos := v_next + 1;
        v_idx := v_idx + 1;
      END IF;
    END;
  END LOOP;

  RETURN v_list;
END;
/

CREATE OR REPLACE PROCEDURE sp_find_grantee_column_privs (
    p_grantee IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT 
            m.GRANTEE,
            m.BASE_TABLE AS TABLE_NAME,
            column_value AS COLUMN_NAME,
            m.PRIVILEGE_TYPE AS PRIVILEGE,
            m.VIEW_NAME,
            m.CREATED_AT
        FROM GRANTED_VIEW_METADATA m,
             TABLE(split_columns(m.COLUMNS))
        WHERE m.GRANTEE = UPPER(p_grantee);  -- THÊM dòng này
END;
/



-- Tìm grantee view
CREATE OR REPLACE PROCEDURE sp_find_grantee_view_privs (
    p_grantee IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM DBA_TAB_PRIVS
        WHERE GRANTEE = UPPER(p_grantee)
          AND TABLE_NAME LIKE 'V_%';  -- view được tạo bởi sp_grant_column_view
END;
/




-- *Kiểm tra user hay role
CREATE OR REPLACE PROCEDURE sp_check_user_or_role (
    p_name     IN VARCHAR2,
    p_result   OUT VARCHAR2
)
AUTHID CURRENT_USER
IS
    v_count INTEGER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM DBA_USERS WHERE USERNAME = UPPER(p_name);
    IF v_count > 0 THEN
        p_result := 'USER';
        RETURN;
    END IF;

    SELECT COUNT(*) INTO v_count FROM DBA_ROLES WHERE ROLE = UPPER(p_name);
    IF v_count > 0 THEN
        p_result := 'ROLE';
    ELSE
        p_result := 'NONE';
    END IF;
END;
/

-- Lấy danh sách bảng của user hiện tại
CREATE OR REPLACE PROCEDURE sp_get_user_tables (
    p_cursor OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT TABLE_NAME FROM USER_TABLES ORDER BY TABLE_NAME;
END;
/

-- Lấy danh sách cột
CREATE OR REPLACE PROCEDURE sp_get_columns_of_table (
    p_table_name IN VARCHAR2,
    p_cursor     OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
IS
BEGIN
    OPEN p_cursor FOR
        SELECT COLUMN_NAME
        FROM USER_TAB_COLUMNS
        WHERE TABLE_NAME = UPPER(p_table_name)
        ORDER BY COLUMN_ID;
END;
/


-- 1. Cấp quyền mức bảng
CREATE OR REPLACE PROCEDURE sp_grant_table_priv (
    p_grantee         IN VARCHAR2,
    p_table           IN VARCHAR2,
    p_privilege       IN VARCHAR2,
    p_with_grant_opt  IN BOOLEAN
)
AUTHID CURRENT_USER
IS
    v_sql VARCHAR2(1000);
BEGIN
    v_sql := 'GRANT ' || p_privilege || ' ON ' || p_table || ' TO ' || p_grantee;

    IF p_with_grant_opt THEN
        v_sql := v_sql || ' WITH GRANT OPTION';
    END IF;

    EXECUTE IMMEDIATE v_sql;
END;
/

-- Cấp quyền User (Insert, Delete)
CREATE OR REPLACE PROCEDURE sp_grant_column_priv (
    p_grantee         IN VARCHAR2,
    p_table           IN VARCHAR2,
    p_columns         IN VARCHAR2,
    p_privilege       IN VARCHAR2,
    p_with_grant_opt  IN BOOLEAN
)
AUTHID CURRENT_USER
IS
    v_sql VARCHAR2(1000);
BEGIN
    EXECUTE IMMEDIATE 'ALTER SESSION SET CURRENT_SCHEMA = admin';

    v_sql := 'GRANT ' || p_privilege || ' (' || p_columns || ') ON ' || p_table || ' TO "' || p_grantee || '"';

    IF p_with_grant_opt THEN
        v_sql := v_sql || ' WITH GRANT OPTION';
    END IF;

    EXECUTE IMMEDIATE v_sql;

EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20001, 'Lỗi trong sp_grant_column_priv: ' || SQLERRM);
END;
/

-- Cấp quyền mức cột (Select, Update)
CREATE OR REPLACE PROCEDURE sp_grant_column_view (
    p_grantee   IN VARCHAR2,
    p_table     IN VARCHAR2,
    p_columns   IN VARCHAR2,
    p_privilege IN VARCHAR2
)
AUTHID CURRENT_USER
IS
    v_view_name VARCHAR2(100);
    v_sql       VARCHAR2(1000);
BEGIN
    -- Tạo tên view giả lập (tránh trùng)
    v_view_name := 'V_' || p_table || '_' || p_grantee;

    -- Xoá nếu đã tồn tại
    BEGIN
        EXECUTE IMMEDIATE 'DROP VIEW ' || v_view_name;
    EXCEPTION
        WHEN OTHERS THEN NULL;
    END;

    -- Tạo view chỉ gồm các cột được cấp
    v_sql := 'CREATE VIEW ' || v_view_name || ' AS SELECT ' || p_columns || ' FROM ' || p_table;
    EXECUTE IMMEDIATE v_sql;

    -- Cấp quyền SELECT cho user/role trên view đó
    v_sql := 'GRANT ' || p_privilege || ' ON ' || v_view_name || ' TO "' || p_grantee || '"';
    EXECUTE IMMEDIATE v_sql;

    -- Ghi lại metadata
    INSERT INTO GRANTED_VIEW_METADATA (GRANTEE, VIEW_NAME, BASE_TABLE, COLUMNS, PRIVILEGE_TYPE)
    VALUES (p_grantee, v_view_name, p_table, p_columns, p_privilege);

    COMMIT;
END;
/



-- 2. Thu hồi quyền
conn admin/06092004@localhost:1521/DaiHocX;
CREATE OR REPLACE PROCEDURE sp_check_view_privilege (
    p_grantee   IN  VARCHAR2,
    p_viewname  IN  VARCHAR2,
    p_privilege IN  VARCHAR2,
    p_result    OUT NUMBER
)
IS
BEGIN
    SELECT 1 INTO p_result
    FROM GRANTED_VIEW_METADATA
    WHERE UPPER(GRANTEE) = UPPER(p_grantee)
      AND UPPER(VIEW_NAME) = UPPER(p_viewname)
      AND UPPER(PRIVILEGE_TYPE) = UPPER(p_privilege);

EXCEPTION
    WHEN NO_DATA_FOUND THEN
        p_result := 0;
END;
/

CREATE OR REPLACE PROCEDURE sp_revoke_column_view (
    p_grantee   IN VARCHAR2,
    p_table     IN VARCHAR2,
    p_privilege IN VARCHAR2
)
AUTHID CURRENT_USER
IS
    v_view_name VARCHAR2(100);
    v_sql       VARCHAR2(1000);
BEGIN
    v_view_name := 'V_' || p_table || '_' || p_grantee;

    -- Thu hồi quyền trên view
    BEGIN
        v_sql := 'REVOKE ' || p_privilege || ' ON ' || v_view_name || ' FROM "' || p_grantee || '"';
        EXECUTE IMMEDIATE v_sql;
    EXCEPTION
        WHEN OTHERS THEN
            NULL; -- View chưa được cấp hoặc quyền đã bị thu hồi
    END;

    -- Xóa view
    BEGIN
        EXECUTE IMMEDIATE 'DROP VIEW ' || v_view_name;
    EXCEPTION
        WHEN OTHERS THEN
            NULL; -- View đã không tồn tại
    END;

    -- ❗️Xóa metadata
    DELETE FROM GRANTED_VIEW_METADATA
    WHERE GRANTEE = p_grantee
      AND VIEW_NAME = v_view_name
      AND PRIVILEGE_TYPE = p_privilege;

    COMMIT;
END;
/

-- 2. Thu hồi quyền
conn admin/06092004@localhost:1521/DaiHocX;
-- Thu hồi quyền mức bảng
CREATE OR REPLACE PROCEDURE sp_revoke_privilege (
    p_grantee   IN VARCHAR2,
    p_table     IN VARCHAR2,
    p_privilege IN VARCHAR2
)
AUTHID CURRENT_USER
IS
    v_sql VARCHAR2(500);
BEGIN
    v_sql := 'REVOKE ' || p_privilege || ' ON ' || p_table || ' FROM "' || p_grantee || '"';
    EXECUTE IMMEDIATE v_sql;
END;
/


--III. Cấp quyền cho Role
-- ***Form PhanHe1_GrantRevokeRoleForUser***
-- 1. Lấy danh sách role mà 1 user đang có
CREATE OR REPLACE PROCEDURE sp_find_roles_of_user (
    p_user IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM DBA_ROLE_PRIVS WHERE GRANTEE = UPPER(p_user);
END;
/

-- 2. Lấy danh sách user đã được gán 1 role nào đó
CREATE OR REPLACE PROCEDURE sp_find_users_of_role (
    p_role IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM DBA_ROLE_PRIVS WHERE GRANTED_ROLE = UPPER(p_role);
END;
/

-- 3. Lấy toàn bộ phân quyền role
CREATE OR REPLACE PROCEDURE sp_get_all_role_privs (
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM DBA_ROLE_PRIVS;
END;
/

---- ***Form PhanHe1_GrantRole***
CREATE OR REPLACE PROCEDURE sp_grant_role_to_user_or_role (
    p_grantee       IN VARCHAR2,
    p_role          IN VARCHAR2,
    p_with_option   IN BOOLEAN
)
AUTHID CURRENT_USER
IS
    v_sql VARCHAR2(500);
BEGIN
    v_sql := 'GRANT ' || p_role || ' TO ' || p_grantee;

    IF p_with_option THEN
        v_sql := v_sql || ' WITH ADMIN OPTION';
    END IF;

    EXECUTE IMMEDIATE v_sql;
END;
/

-- ***Form PhanHe1_RevokeRole***
CREATE OR REPLACE PROCEDURE sp_revoke_role_from_user_or_role (
    p_grantee IN VARCHAR2,
    p_role    IN VARCHAR2
)
AUTHID CURRENT_USER
IS
    v_sql VARCHAR2(400);
BEGIN
    v_sql := 'REVOKE ' || p_role || ' FROM ' || p_grantee;
    EXECUTE IMMEDIATE v_sql;
END;
/