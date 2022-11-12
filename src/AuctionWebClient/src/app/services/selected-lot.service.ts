import { Injectable } from '@angular/core';
import {LotResponse} from "../responses/lot-response";
import {Guid} from "guid-typescript";
import {LotService} from "./lot.service";

@Injectable({
  providedIn: 'root'
})
export class SelectedLotService {
  private selectedLot? : LotResponse;

  constructor(private lotService : LotService) { }

  getSelectedLot() : LotResponse | undefined {
    return this.selectedLot;
  }

  setSelectedLot(id : Guid){
    this.lotService.getById(id).subscribe( lot => {
      this.selectedLot = lot;
    });
  }
}
