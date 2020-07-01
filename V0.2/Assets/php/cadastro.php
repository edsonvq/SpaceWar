<?php
session_start();
require_once("conex.php");

$nick = $_POST["nick"];
$login = $_POST["login"];
$senha = $_POST["password"];


$data['data']['status'] = 'error';
$data['data']['msg'] = '';
$data['data']['id_user'] = '';


if (!empty($nick) && !empty($login) && !empty($senha)) {

	if (strlen($nick) > 8) {
		$data['data']['msg'] = 'Seu nick deve ter no maximo oito caracteres!';
		echo json_encode($data);
		exit;
	}


    $query =  mysqli_query($conn, "SELECT * FROM `user` WHERE LOWER(login)='".strtolower($login)."'");
    $rowcount1=mysqli_num_rows($query);

    if($rowcount1 > 0){
        $data['data']['msg'] = "Esse login já está cadastrado no banco de dados.";
		
        echo json_encode($data);
    }

    $query =  mysqli_query($conn, "SELECT * FROM `user` WHERE LOWER(nick)='".strtolower($nick)."'");
    $rowcount2=mysqli_num_rows($query);

    if($rowcount2 > 0){
        $data['data']['msg'] = "Esse nick já está cadastrado no banco de dados.";
		
        echo json_encode($data);
    }
	
	if(($rowcount1+$rowcount2) == 0){
		$data_ = date("Y-m-d H:i:s", strtotime('-2 hours'));
		$query =  mysqli_query($conn, "INSERT INTO user (login,password,nick,created_at) VALUES ('".$login."', '".$senha."', '".$nick."', '".$data_."')");

		if ($query) {

			$result = mysqli_query($conn, "SELECT id FROM user where login = '".$login."'");
			while ($row = mysqli_fetch_assoc($result)) {
				$data['data']['id_user'] = $row["id"];
			}
			
		
			$result =  mysqli_query($conn, "SELECT max(id) as id FROM game");
			while($row = mysqli_fetch_assoc($result)) {
				$id_game = $row["id"];
			}
			$id_game++;

			mysqli_query($conn, "INSERT INTO game (id_user,win,score,created_at) 
			VALUES ('".$data['data']['id_user']."', '0', '0', '".$data_."')");
		
	
			$data['data']['status'] = 'success';
			$data['data']['msg'] = "Cadastro realizado com sucesso.";
			echo json_encode($data);
		} else {
			$data['data']['msg'] = "Não foi possível realizar seu cadastro no momento.";
			echo json_encode($data);
		}
	}
	
    mysqli_close($conn);
	
}

