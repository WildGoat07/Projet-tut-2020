<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];
$firstValue = true;

foreach ($postObj->values as $values) {

    $strReq = "UPDATE `semestre` SET ";

    $data = $values->data;

    if (isset($data->code_sem)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_sem` = '$data->code_sem'";
    }
    if (isset($data->libelle_sem)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`libelle_sem` = '$data->libelle_sem'";
    }
    if (isset($data->no_sem)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`no_sem` = '$data->no_sem'";
    }
    if (isset($data->code_etape)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_etape` = '$data->code_etape'";
    }
    if (isset($data->vet)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`vet` = '$data->vet'";
    }

    $target = $values->target;
    $strReq .= " WHERE `code_sem` = '$target->code_sem' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet` FROM `semestre` WHERE ";
            if (isset($data->code_sem))
                $resultStr .= "`code_sem` = '$data->code_sem'";
            else
                $resultStr .= "`code_sem` = '$target->code_sem'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_sem = $row->code_sem;
            $obj->libelle_sem = $row->libelle_sem;
            $obj->no_sem = $row->no_sem;
            $obj->code_etape = $row->code_etape;
            $obj->vet = $row->vet;

            $returnedValues->values[] = $obj;
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);
