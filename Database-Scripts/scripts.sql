/* ---- Inserting roles ------ */
INSERT INTO `productmanager`.`roles`
(`CreatedOn`,
`ModifiedOn`,
`Name`)
VALUES
(
current_timestamp(),
current_timestamp(),
"Admin");

INSERT INTO `productmanager`.`roles`
(`CreatedOn`,
`ModifiedOn`,
`Name`)
VALUES
(
current_timestamp(),
current_timestamp(),
"User");


/* ---- Inserting users(Admin) ------ */
INSERT INTO `productmanager`.`users`
(
`CreatedOn`,
`Email`,
`FirstName`,
`LastName`,
`ModifiedOn`,
`Password`,
`RoleId`,
`Salt`,
`Username`)
VALUES
(
current_timestamp(),
"himanshu.uppal@nagarro.com",
"Himanshu",
"Uppal",
current_timestamp(),
"A0ted/kglOSKrSLH9KMbP6naOyqEP6x/hSulBnYrOI0=",
1,
"nAzsp54PDiVsV70UKBELGQ==",
"himanshu");


/* ---- Inserting Products ------ */


