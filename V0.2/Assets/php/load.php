<?php


session_start();
header('Content-Type: application/json');

require_once("conex.php");

$type = $_GET["type"];

//Count games by nivel - user
if($type == 1){
    $sqlQuery = "SELECT difficulty, COUNT(id) as total FROM game WHERE id_user = ".$_SESSION["id"]." group by difficulty";

    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['game'][] = $row;
            $total += $row['total'];
    }
    
    $data['total'] = $total;
    mysqli_close($conn);

    echo json_encode($data);
}

//Count win by nivel - user
if($type == 2){
    $sqlQuery1 = "SELECT difficulty, COUNT(id) as total FROM game WHERE id_user = ".$_SESSION["id"]." AND win = 1 group by difficulty";
    $result1 = mysqli_query($conn,$sqlQuery1);
    
    $sqlQuery2 = "SELECT difficulty, COUNT(id) as total FROM game WHERE id_user = ".$_SESSION["id"]." AND win = 0 group by difficulty";
    $result2 = mysqli_query($conn,$sqlQuery2);
    
    $data = array();
    $total_1 = 0;
    foreach ($result1 as $row1) {
            $data['wins_by_nivel'][] = $row1;
            $total_1 += $row1['total'];
    }
    
    $total_2 = 0;
    foreach ($result2 as $row2) {
            $data['lose_by_nivel'][] = $row2;
            $total_2 += $row2['total'];
    }
    
    $data['total_win'] = $total_1;
    $data['total_lose'] = $total_2;
    
    mysqli_close($conn);

    echo json_encode($data);
}

//Count cores by win - user
if($type == 3){
    $sqlQuery = "
        SELECT c.color, COUNT(c.id) as total FROM game a, game_attempts b, game_attempts_colors c 
            WHERE a.id_user = ".$_SESSION["id"]." AND a.win = 1 AND 
            a.id = b.id_game AND 
            b.id = c.id_attempt AND 
            a.round_win = b.round 
            group by c.color";
    
    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['colors_by_wins'][] = $row;
            $total += $row['total'];
    }
    $data['total'] = $total;
    
    mysqli_close($conn);

    echo json_encode($data);
}



//Count tentativas by nivel - user
if($type == 4){
    $sqlQuery = "SELECT a.difficulty, count(b.id) as total FROM game a, game_attempts b 
        WHERE a.id_user = ".$_SESSION["id"]." AND a.id = b.id_game GROUP BY a.difficulty";
    $result = mysqli_query($conn,$sqlQuery);

    $data = array();
    $total = 0;

    foreach ($result as $row) {
            $data['difficulty'][] = $row;
            $total += $row['total'];

    }
    $data['total'] = $total;

    mysqli_close($conn);

    echo json_encode($data);
}

//Count cores - user
if($type == 5){
    $sqlQuery = "
	SELECT c.color, COUNT(c.color) as total FROM game a, game_attempts b,  game_attempts_colors c WHERE
    a.id_user = ".$_SESSION["id"]." and 
    a.id = b.id_game and 
    b.id = c.id_attempt 
    group by c.color order by c.color ASC";

    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['colors'][] = $row;
            $total += $row['total'];
    }
    
    $data['total'] = $total;


    mysqli_close($conn);

    echo json_encode($data);
}

//Count cores by nivel - user
if($type == 6){
    $sqlQuery = "
	SELECT a.difficulty, COUNT(c.color) as total FROM game a, game_attempts b,  game_attempts_colors c WHERE
    a.id_user = ".$_SESSION["id"]." and 
    a.id = b.id_game and 
    b.id = c.id_attempt 
    group by a.difficulty order by a.difficulty ASC";

    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['colors_by_nivel'][] = $row;
            $total += $row['total'];
    }
    
    $data['total'] = $total;


    mysqli_close($conn);

    echo json_encode($data);
}



//Count cores - geral
if($type == 11){
    $sqlQuery = "SELECT color, COUNT(color) as total FROM game_attempts_colors group by color order by color ASC";

    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
        $data['colors_geral'][] = $row;
        $total += $row['total'];
    }
	
    $data['total'] = $total;
    mysqli_close($conn);

    echo json_encode($data);
}

//Count games by nivel - geral
if($type == 12){
    $sqlQuery = "SELECT difficulty, COUNT(id) as total FROM game group by difficulty";

    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['game_geral'][] = $row;
            $total += $row['total'];
    }
    
    $data['total'] = $total;
    mysqli_close($conn);

    echo json_encode($data);
}

//Count win by nivel - geral
if($type == 13){
    $sqlQuery1 = "SELECT difficulty, COUNT(id) as total FROM game WHERE win = 1 group by difficulty";
    $result1 = mysqli_query($conn,$sqlQuery1);
    
    $sqlQuery2 = "SELECT difficulty, COUNT(id) as total FROM game WHERE win = 0 group by difficulty";
    $result2 = mysqli_query($conn,$sqlQuery2);
    
    $data = array();
    $total_1 = 0;
    foreach ($result1 as $row1) {
            $data['wins_by_nivel_geral'][] = $row1;
            $total_1 += $row1['total'];
    }
    
    $total_2 = 0;
    foreach ($result2 as $row2) {
            $data['lose_by_nivel_geral'][] = $row2;
            $total_2 += $row2['total'];
    }
    
    $data['total_win'] = $total_1;
    $data['total_lose'] = $total_2;
    
    mysqli_close($conn);

    echo json_encode($data);
}

//Count cores by win - geral
if($type == 14){
    $sqlQuery = "SELECT c.color, COUNT(c.id) as total FROM game a, game_attempts b, game_attempts_colors c 
            WHERE a.win = 1 AND 
            a.id = b.id_game AND 
            b.id = c.id_attempt AND 
            a.round_win = b.round 
            group by c.color
        ";
    
    $result = mysqli_query($conn,$sqlQuery);
    
    $data = array();
    $total = 0;
    foreach ($result as $row) {
            $data['colors_by_wins_geral'][] = $row;
            $total += $row['total'];
    }
    $data['total'] = $total;
    
    mysqli_close($conn);

    echo json_encode($data);
}
?>