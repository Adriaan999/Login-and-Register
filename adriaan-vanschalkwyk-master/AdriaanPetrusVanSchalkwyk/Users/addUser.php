<?php
require('includes/dbconf.php');//has db connection info

$json = file_get_contents('php://input');
$obj = json_decode($json);

if(!is_null($obj))
{
	// Check connection
	if ($dbsqli->connect_error) {
		die("Connection failed: " . $dbsqli->connect_error);
	}

	$sql = "INSERT INTO login(username,password)
						VALUES (
								'".$obj->{'username'}."',
								'".$obj->{'password'}."')";

	if ($dbsqli->query($sql) === TRUE) {
		echo "success";
	} else {
		echo "Error: " . $sql . "<br>" . $dbsqli->error;
	}

	$dbsqli->close();
}
else
{
	echo "OBJECT UNDEFINED. CHECK JSON";
}
?>