<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    
    $postObj = json_decode(utf8_encode(file_get_contents('php://input')));

    $presence = new stdClass();
    $presence->code_ue = false;
    $presence->libelle_ue = false;
    $presence->nature = false;
    $presence->ECTS = false;
    $presence->code_ue_pere = false;
    $presence->code_sem = false;

    $returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->succes=false;

    foreach ($postObj->values as $values) {
        $strReq = "INSERT INTO `ue` (";
        $data = "(";

        $strReq .= " `code_ue` ";
        $data .= "'$values->code_ue'";

        $strReq .= " ,`libelle_ue` ";
        $data .= ",'$values->libelle_ue'";

        
        if (isset($values->nature)) {
            $strReq .= " ,`nature` ";
            $data .= ",'$values->nature'";
        }

        if (isset($values->ECTS)) {
            $strReq .= " ,`ECTS` ";
            $data .= ",'$values->ECTS'";
        }

        if (isset($values->code_ue_pere)) {
            $strReq .= " ,`code_ue_pere` ";
            $data .= ",'$values->code_ue_pere'";
        }

        if (isset($values->code_sem)) {
            $strReq .= " ,`code_sem`";
            $data .= ",'$values->code_sem'";
        }

        $strReq .= ") VALUES $data )";
    
        $result = $db->query($strReq);
        
        if($result) {
            //on retourne le denrier résultat enregistré
            $getLast = $db->query("SELECT `code_ue`,`libelle_ue`,`nature`,`ECTS`,`code_ue_pere`,`code_sem` FROM `ue`
            ORDER BY code_ue DESC
            LIMIT 1; ");

            $result = $getLast->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_ue = $result->code_ue;
            $obj->libelle_ue = $result->libelle_ue;
            $obj->nature = $result->nature;
            $obj->ECTS = $result->ECTS;
            $obj->code_ue_pere = $result->code_ue_pere;
            $obj->code_sem = $result->code_sem;

            $returnedValues->values[] = $obj;
            $returnedValues->succes=true;
        }
    }

    echo json_encode($returnedValues);
} else {
    echo json_encode($connectionDB);
}