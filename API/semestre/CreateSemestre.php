<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered_code_sem = $values->code_sem;

    $strReq = "INSERT INTO `semestre` (";
    $data = "(";

    $strReq .= " `code_sem` ";
    $data .= "'$values->code_sem'";

    $strReq .= " ,`libelle_sem` ";
    $data .= ",'$values->libelle_sem'";

    if (isset($values->no_sem)) {
        $strReq .= " ,`no_sem` ";
        $data .= ",'$values->no_sem'";
    }

    if (isset($values->code_etape)) {
        $strReq .= " ,`code_etape` ";
        $data .= ",'$values->code_etape'";
    }

    if (isset($values->vet)) {
        $strReq .= " ,`vet` ";
        $data .= ",'$values->vet'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();

    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet` FROM `semestre` WHERE ";
            $resultStr .= "`code_sem` = '$id_entered_code_sem'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_sem = $row->code_sem;
            $obj->libelle_sem = $row->libelle_sem;
            $obj->no_sem = $row->no_sem;
            $obj->code_etape = $row->code_etape;
            $obj->vet = $row->vet;
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