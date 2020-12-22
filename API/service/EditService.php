<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = false;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `service` SET ";

    $data = $values->data;

    if (isset($data->id_ens))
        $strReq .= "`id_ens` = '$data->id_ens'";

    if (isset($data->code_ec))
        $strReq .= "`code_ec` = '$data->code_ec'";

    if (isset($data->annee))
        $strReq .= "`annee` = '$data->annee'";

    if (isset($data->NbGpCM))
        $strReq .= "`NbGpCM` = '$data->NbGpCM'";

    if (isset($data->NbGpEI))
        $strReq .= "`NbGpEI` = '$data->NbGpEI'";

    if (isset($data->NBGpTD))
        $strReq .= "`NBGpTD` = '$data->NBGpTD'";

    if (isset($data->NbGpTP))
        $strReq .= "`NbGpTP` = '$data->NbGpTP'";

    if (isset($data->NbGpTPL))
        $strReq .= "`NbGpTPL` = '$data->NbGpTPL'";

    if (isset($data->NBGpPRJ))
        $strReq .= "`NBGpPRJ` = '$data->NBGpPRJ'";

    if (isset($data->HEqTD))
        $strReq .= "`HEqTD` = '$data->HEqTD'";


    $target = $values->target;
    $strReq .= " WHERE `id_ens` = '$target->id_ens' AND `code_ec` = '$target->code_ec' AND `annee`= '$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEI`, `NBGpTD`, `NbGbTP`, `NbGpTPL`, `NBGpPRJ`, `HEqTD` FROM `service` WHERE ";

            $firstValue = true;
            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->id_ens))
                $resultStr .= "(`id_ens` = '$data->id_ens')";
            else
                $resultStr .= "(`id_ens` = '$target->id_ens')";

            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->code_ec))
                $resultStr .= "(`code_ec` = '$data->code_ec')";
            else
                $resultStr .= "(`code_ec` = '$target->code_ec')";

            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if (isset($data->annee))
                $resultStr .= "(`annee` = '$data->annee')";
            else
                $resultStr .= "(`annee` = '$target->annee')";

            $result = $db->query($resultStr);
            $error = $result->errorInfo();

            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_ens = $row->id_ens;
            $obj->code_ec = $row->code_ec;
            $obj->annee = $row->annee;
            $obj->NbGpCM = $row->NbGpCM;
            $obj->NbGpEI = $row->NbGpEI;
            $obj->NBGpTD = $row->NBGpTD;
            $obj->NbGbTP = $row->NbGbTP;
            $obj->NbGpTPL = $row->NbGpTPL;
            $obj->NBGpPRJ = $row->NBGpPRJ;
            $obj->HeqTD = $row->HEqTD;

            $returnedValues->values[] = $obj;
            $returnedValues->success = true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    } else {
        $error = $updateReq->errorInfo();

        $obj = new stdClass();

        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);
