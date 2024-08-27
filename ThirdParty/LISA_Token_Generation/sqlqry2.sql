BEGIN
DBMS_NETWORK_ACL_ADMIN.add_privilege (
acl => 'summit_oas_acl.xml',
principal => 'CVC',
is_grant => TRUE,
privilege => 'connect',
start_date => NULL,
end_date => NULL);
COMMIT;
END;
/
exit