<?php
$host = "localhost";
$usuario = "u213531759_war";
$senha = "2811199711";
$banco = "u213531759_war";

$conn = mysqli_connect($host, $usuario, $senha) or die(mysqli_error());
$db = mysqli_select_db($conn, $banco) or die(mysqli_error());

mysqli_set_charset($conn,"utf8");

    function anti_injection($x) {
        // remove palavras que contenham sintaxe sql
        $x = preg_replace("/(from|select|insert|delete|where|drop table|show tables|#|\*|--|\\\\)/", "", $x);
        $x = trim($x); //limpa espaços vazio
        $x = strip_tags($x); //tira tags html e php
        $x = addslashes($x); //Adiciona barras invertidas a uma string
        return $x;
    }
?>
