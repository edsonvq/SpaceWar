<?php
session_start();
require_once("conex.php");

$login = $_POST["login"];
$senha = $_POST["senha"];

$data = array();
$data['status'] = 'error';
$data['msg'] = '';
    
if (empty($login)) {
    $data['msg'] .= "Digite seu login<br>";
} 
elseif (empty($senha)) {
    $data['msg'] .= "Digite sua senha<br>";
} 
else {
    $pesquisa = mysqli_query($conn, "SELECT * FROM `users` WHERE login = '$login' AND password = '$senha'");
    $confirmacao = mysqli_num_rows($pesquisa);

    mysqli_close($conn);

    if ($confirmacao == 1) {
        while ($row = mysqli_fetch_array($pesquisa)) {
            
            $data = date("Y-m-d H:i:s", strtotime('-2 hours'));
            
            mysqli_query($conn, "UPDATE `users` SET updated_at = ".$data." WHERE id = '".$row["id"]."'");
            
            $_SESSION["id"] = $row["id"];
            $_SESSION["nick"] = $row["nick"];
            $_SESSION["erro"] = 0;

            $data['status'] = 'success';
            echo json_encode($data);
        }
    } elseif ($confirmacao !== 1) {
        $_SESSION["erro"] = 1;
        $data['msg'] = "Login ou senha incorretos<br>";
            
        echo json_encode($data);
    }

}
?>


 
