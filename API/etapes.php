<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "SELECT `code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi` FROM `etape`";
    $postObj = json_decode(file_get_contents('php://input'));
    if (isset($postObj->filters)) {
        $firstFilter = true;
        $whereSet = false;
        if (isset($postObj->filters->code_etape)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->code_etape as $code_etape) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`code_etape` = \"$code_etape\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->vet)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->vet as $vet) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`vet` = \"$vet\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->id_comp)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->id_comp as $id_comp) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`id_comp` = \"$id_comp\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
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
    }

    if (isset($postObj->order))
        if (isset($postObj->reverse_order) && $postObj->reverse_order)
            $strReq .= " ORDER BY DESC `$postObj->order`";
        else
            $strReq .= " ORDER BY `$postObj->order`";
    else if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY DESC `code_etape`";
    else
        $strReq .= " ORDER BY `code_etape`";
    $strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

    $requete = $db->query($strReq);

    $etape = new stdClass();
    $etape->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_etape = utf8_encode($req['code_etape']);

        $obj->vet = utf8_encode($req['vet']);

        $obj->libelle_vet = utf8_encode($req['libelle_vet']);

        $obj->id_comp = utf8_encode($req['id_comp']);

        $obj->code_diplome = utf8_encode($req['code_diplome']);

        $obj->vdi = utf8_encode($req['vdi']);

        $etape->values[] = $obj;
    }

    $etape->success = true;

    echo json_encode($etape);
} else {
    echo json_encode($connectionDB);
}
