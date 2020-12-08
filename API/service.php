<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEl`, `NBGpTD`, `NbGbTP`, `NbGpTPL`, `NBGpPRJ`, `HEqTD` FROM `service`";
    $postObj = json_decode(file_get_contents('php://input'));
    if (isset($postObj->filters)) {
        $firstFilter = true;
        $whereSet = false;
        if (isset($postObj->filters->id_ens)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->id_ens as $id_ens) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`id_ens` = \"$id_ens\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->code_ec)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->code_ec as $code_ec) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`code_ec` = \"$code_ec\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->annee)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->annee as $annee) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`annee` = \"$annee\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->NbGpCM)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NbGpCM->min)) {
                $strReq .= "`NbGpCM` >= " . $postObj->filters->NbGpCM->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NbGpCM->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NbGpCM` <= " . $postObj->filters->NbGpCM->max;
            }
        }
        if (isset($postObj->filters->NbGpEl)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NbGpEl->min)) {
                $strReq .= "`NbGpEl` >= " . $postObj->filters->NbGpEl->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NbGpEl->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NbGpEl` <= " . $postObj->filters->NbGpEl->max;
            }
        }
        if (isset($postObj->filters->NBGpTD)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NBGpTD->min)) {
                $strReq .= "`NBGpTD` >= " . $postObj->filters->NBGpTD->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NBGpTD->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NBGpTD` <= " . $postObj->filters->NBGpTD->max;
            }
        }
        if (isset($postObj->filters->NbGpTP)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NbGpTP->min)) {
                $strReq .= "`NbGpTP` >= " . $postObj->filters->NbGpTP->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NbGpTP->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NbGpTP` <= " . $postObj->filters->NbGpTP->max;
            }
        }
        if (isset($postObj->filters->NbGpTPL)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NbGpTPL->min)) {
                $strReq .= "`NbGpTPL` >= " . $postObj->filters->NbGpTPL->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NbGpTPL->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NbGpTPL` <= " . $postObj->filters->NbGpTPL->max;
            }
        }
        if (isset($postObj->filters->NBGpPRJ)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->NBGpPRJ->min)) {
                $strReq .= "`NBGpPRJ` >= " . $postObj->filters->NBGpPRJ->min;
                $minSet = true;
            }
            if (isset($postObj->filters->NBGpPRJ->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`NBGpPRJ` <= " . $postObj->filters->NBGpPRJ->max;
            }
        }
        if (isset($postObj->filters->HEqTD)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->HEqTD->min)) {
                $strReq .= "`HEqTD` >= " . $postObj->filters->HEqTD->min;
                $minSet = true;
            }
            if (isset($postObj->filters->HEqTD->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`HEqTD` <= " . $postObj->filters->HEqTD->max;
            }
        }

        if (isset($postObj->order))
            if (isset($postObj->reverse_order) && $postObj->reverse_order)
                $strReq .= " ORDER BY DESC `$postObj->order`";
            else
                $strReq .= " ORDER BY `$postObj->order`";
        else if (isset($postObj->reverse_order) && $postObj->reverse_order)
            $strReq .= " ORDER BY DESC `id_ens`";
        else
            $strReq .= " ORDER BY `id_ens`";
        $strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

        $requete = $db->query($strReq);

        $service = new stdClass();
        $service->values = [];

        foreach ($requete as $req) {
            $obj = new stdClass();

            $obj->id_ens = utf8_encode($req['id_ens']);

            $obj->code_ec = utf8_encode($req['code_ec']);

            $obj->annee = utf8_encode($req['annee']);

            $obj->NbGpCM = utf8_encode($req['NbGpCM']);

            $obj->NbGpEI = utf8_encode($req['NbGpEI']);

            $obj->NbGpTD = utf8_encode($req['NBGpTD']);

            $obj->NbGpTP = utf8_encode($req['NbGpTP']);

            $obj->NbGpTPL = utf8_encode($req['NbGpTPL']);

            $obj->NBGpPRJ = utf8_encode($req['NBGpPRJ']);

            $obj->NbHEqTDGp = utf8_encode($req['HEqTD']);

            $service->values[] = $obj;
        }

        $service->success = true;

        echo json_encode($service);
    } else {
        echo json_encode($connectionDB);
    }
}
