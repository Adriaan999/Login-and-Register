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

	$sql = mysqli_query($dbsqli,"select * from login where username ='".$obj->{'username'}."' AND password ='".$obj->{'password'}."' ");

	
	if (mysqli_num_rows($sql) > 0) {
		echo "success";
	} else {
		echo "unsuccessful";
		
	}

	$dbsqli->close();
}
else
{
	echo "OBJECT UNDEFINED. CHECK JSON";
}
?>