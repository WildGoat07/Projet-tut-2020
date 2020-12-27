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

    $strReq = "UPDATE `enseignant` SET ";

    $data = $values->data;

    if (isset($data->id_ens)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`id_ens` = '$data->id_ens'";
    }
    if (isset($data->nom)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`nom` = '$data->nom'";
    }
    if (isset($data->prenom)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`prenom` = '$data->prenom'";
    }
    if (isset($data->fonction)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`fonction` = '$data->fonction'";
    }
    if (isset($data->HOblig)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HOblig` = '$data->HOblig'";
    }
    if (isset($data->HMax)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HMax` = '$data->HMax'";
    }
    if (isset($data->CRCT)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`CRCT` = '$data->CRCT'";
    }
    if (isset($data->PES_PEDR)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`PES_PEDR` = '$data->PES_PEDR'";
    }
    if (isset($data->id_comp)) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`id_comp` = '$data->id_comp'";
    }

    $target = $values->target;
    $strReq .= " WHERE `id_ens` = '$target->id_ens' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp` FROM `enseignant` WHERE ";
            if (isset($data->id_ens))
                $resultStr .= "`id_ens` = '$data->id_ens'";
            else
                $resultStr .= "`id_ens` = '$target->id_ens'";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_ens = $row->id_ens;
            $obj->nom = $row->nom;
            $obj->prenom = $row->prenom;
            $obj->fonction = $row->fonction;
            $obj->HOblig = $row->HOblig;
            $obj->HMax = $row->HMax;
            $obj->CRCT = $row->CRCT;
            $obj->PES_PEDR = $row->PES_PEDR;
            $obj->id_comp = $row->id_comp;

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
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);