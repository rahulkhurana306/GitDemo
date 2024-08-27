--Register all jboss ip addresses / host names of test environments
BEGIN
DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
Acl => 'summit_oas_acl.xml',
Host => 'dlnxmss5eap02.woki.uk.ssg',
lower_port => null,
upper_port => null);

DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
Acl => 'summit_oas_acl.xml',
Host => 'dlnxmss56eap01.noid.in.ssg',
lower_port => null,
upper_port => null);

DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
Acl => 'summit_oas_acl.xml',
Host => 'dlnxmss5eap01.noid.in.ssg',
lower_port => null,
upper_port => null);

--Mock services are deployed on swinmssauto09 using Jenkin MockServices JOB
DBMS_NETWORK_ACL_ADMIN.ASSIGN_ACL (
Acl => 'summit_oas_acl.xml',
Host => 'swinmssauto09.noid.in.ssg',
lower_port => null,
upper_port => null);
END;
/
exit