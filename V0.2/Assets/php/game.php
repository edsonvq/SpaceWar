<?php
require_once("conex.php");

$user = $_POST["id"];
$win = $_POST["win"];

$score = $_POST["score"];


$result =  mysqli_query($conn, "SELECT max(id) as id FROM game");
while($row = mysqli_fetch_assoc($result)) {
    $id_game = $row["id"];
}
$id_game++;

$data = date("Y-m-d H:i:s", strtotime('-2 hours'));

mysqli_query($conn, "DELETE FROM game WHERE id_user = ".$user." AND score = 0");
mysqli_query($conn, "INSERT INTO game (id_user,win,score,created_at) 
VALUES ('".$user."', '".$win."', '".$score."', '".$data."')");

$data['data']['status'] = 'success';
echo json_encode($data);
		
mysqli_close($conn);

?>