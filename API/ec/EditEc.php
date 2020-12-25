<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];

foreach ($postObj->values as $values) {
    $firstValue = true;

    $strReq = "UPDATE `ec` SET ";

    $data = $values->data;

    if (isset($data->code_ec)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_ec` = '$data->code_ec'";
    }
    if (isset($data->libelle_ec)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`libelle_ec` = '$data->libelle_ec'";
    }
    if (isset($data->nature)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`nature` = '$data->nature'";
    }
    if (isset($data->HCM)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HCM` = '$data->HCM'";
    }
    if (isset($data->HEI)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HEI` = '$data->HEI'";
    }
    if (isset($data->HTD)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTD` = '$data->HTD'";
    }
    if (isset($data->HTP)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTP` = '$data->HTP'";
    }
    if (isset($data->HTPL)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTPL` = '$data->HTPL'";
    }
    if (isset($data->HPRJ)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HPRJ` = '$data->HPRJ'";
    }
    if (isset($data->NbEpr)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`NbEpr` = '$data->NbEpr'";
    }
    if (isset($data->CNU)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`CNU` = '$data->CNU'";
    }
    if (isset($data->no_cat)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`no_cat` = '$data->no_cat'";
    }
    if (isset($data->code_ec_pere)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_ec_pere` = '$data->code_ec_pere'";
    }
    if (isset($data->code_ue)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`code_ue` = '$data->code_ue'";
    }

    $target = $values->target;
    $strReq .= " WHERE `code_ec` = '$target->code_ec' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `code_ec`, `libelle_ec`, `nature`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `NbEpr`,
             `CNU`, `no_cat`, `code_ec_pere`, `code_ue` FROM `ec` WHERE ";
            if (isset($data->code_ec))
                $resultStr .= "`code_ec` = '$data->code_ec'";
            else
                $resultStr .= "`code_ec` = '$target->code_ec'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_ec = $row->code_ec;
            $obj->libelle_ec = $row->libelle_ec;
            $obj->nature = $row->nature;
            $obj->HCM = $row->HCM;
            $obj->HEI = $row->HEI;
            $obj->HTD = $row->HTD;
            $obj->HTP = $row->HTP;
            $obj->HTPL = $row->HTPL;
            $obj->HPRJ = $row->HPRJ;
            $obj->NbEpr = $row->NbEpr;
            $obj->CNU = $row->CNU;
            $obj->no_cat = $row->no_cat;
            $obj->code_ec_pere = $row->code_ec_pere;
            $obj->code_ue = $row->code_ue;

            $returnedValues->values[] = $obj;
        } else {
            $returnedValues->success=false;
        
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    } else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);