<?php
ob_start();
session_start();

ini_set('display_errors',1);

	//set timezone
	date_default_timezone_set('africa/johannesburg');

	//database credentials
	define('DBHOST','localhost');
	define('DBUSER','root');
	define('DBPASS','');
	define('DBNAME','userlogin');

	try 
	{
		//create mysqli connection
		$dbsqli = new mysqli(DBHOST, DBUSER, DBPASS, DBNAME);
		
		//create PDO connection
		$db = new PDO("mysql:host=".DBHOST.";dbname=".DBNAME, DBUSER, DBPASS);
		$db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

	} 
	catch(PDOException $e) 
	{
		//show error
		echo '<p class="bg-danger">'.$e->getMessage().'</p>';
		exit;
	}
?>