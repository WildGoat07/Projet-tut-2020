<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$diplome = new stdClass();
$diplome->values = [];
$diplome->success = false;
$diplome->errors = [];

$strReq = "SELECT `code_diplome`, `libelle_diplome`, `vdi`, `libelle_vdi`, `annee_deb`, `annee_fin` FROM `diplome`";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
    if (isset($postObj->filters->code_diplome)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_diplome as $code_diplome) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_diplome` = \"$code_diplome\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->libelle_diplome)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->libelle_diplome as $libelle_dip) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`libelle_diplome` = \"$libelle_dip\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->vdi)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->vdi as $vdi) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`vdi` = \"$vdi\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->libelle_vdi)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->libelle_vdi as $libelle_vdi) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`libelle_vdi` = \"$libelle_vdi\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->annee_deb)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->annee_deb as $annee_deb) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`annee_deb` = \"$annee_deb\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->annee_fin)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->annee_fin as $annee_fin) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`annee_fin` = \"$annee_fin\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_diplome` DESC ";
else
    $strReq .= " ORDER BY `code_diplome`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();


if ($error[0]=='00000') {
    foreach ($requete as $req) {
        $obj = new stdClass();
    
        $obj->code_diplome = utf8_encode($req['code_diplome']);
    
        $obj->libelle_diplome = utf8_encode($req['libelle_diplome']);
    
        $obj->vdi = utf8_encode($req['vdi']);
    
        $obj->libelle_vdi = utf8_encode($req['libelle_vdi']);
    
        $obj->annee_deb = utf8_encode($req['annee_deb']);
    
        $obj->annee_fin = utf8_encode($req['annee_fin']);
    
        $diplome->values[] = $obj;
    }

    $diplome->success = true;
}
else {
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $diplome->errors[] = $obj;
}


echo json_encode($diplome);