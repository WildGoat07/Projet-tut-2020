<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered = $values->code_ue;

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

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();
    
    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem` FROM `ue` WHERE ";
            $resultStr .= "`code_ue` = '$id_entered'";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_ue = $row->code_ue;
            $obj->libelle_ue = $row->libelle_ue;
            $obj->nature = $row->nature;
            $obj->ECTS = $row->ECTS;
            $obj->code_ue_pere = $row->code_ue_pere;
            $obj->code_sem = $row->code_sem;
            
            $returnedValues->values[] = $obj;
        } 
        else {
            $returnedValues->success=false;
            
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    }
    else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);