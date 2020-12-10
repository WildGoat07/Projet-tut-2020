<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered=[];
$indexId=0;

$presence = new stdClass();
    $presence->code_ue = false;
    $presence->libelle_ue = false;
    $presence->nature = false;
    $presence->ECTS = false;
    $presence->code_ue_pere = false;
    $presence->code_sem = false;

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
    $returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `ue` (";
    $data = "(";

    $strReq .= " `code_ue` ";
    $data .= "'$values->code_ue'";
    array_push($id_entered, $values->code_ue);

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

    $requete=$pdo->prepare($strReq);
    
    if ( $requete->execute() ) {
        $resultStr = "SELECT `code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem` FROM `ue` WHERE ";
        $resultStr .= "code_ue = $id_entered[$indexId]";
        $indexId++;

        $result=$pdo->query($resultStr);
        $row=$result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->code_ue = $row->code_ue;
        $obj->libelle_ue = $row->libelle_ue;
        $obj->nature = $row->nature;
        $obj->ECTS = $row->ECTS;
        $obj->code_ue_pere = $row->code_ue_pere;
        $obj->code_sem = $row->code_sem;

        array_push($returnedValues->values, $obj);
        $returnedValues->success=true;
    }
    else {
        $error=$requete->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);