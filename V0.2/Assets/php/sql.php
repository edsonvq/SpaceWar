<?php
session_start();
require_once("conex.php");

$sql = $_POST["sql"];

$data = array();

$data['status'] = 'error';

$query = mysqli_query($conn, $sql);
$rowcount=mysqli_num_rows($query);
	
if ($query) {
	if ($rowcount>0){
		$data['status'] = 'success';
		while ($row = mysqli_fetch_array($query)) {
			$data['data'][] = $row;
		}
	}
}

echo json_encode($data);
mysqli_close($conn);

?>


 
