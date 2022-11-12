import {AfterContentInit, AfterViewInit, Component, DoCheck, OnInit} from '@angular/core';
import {LotService} from "../services/lot.service";
import {SelectedLotService} from "../services/selected-lot.service";
import {LotResponse} from "../responses/lot-response";

@Component({
  selector: 'app-lot-view',
  templateUrl: './lot-view.component.html',
  styleUrls: ['./lot-view.component.css']
})
export class LotViewComponent implements DoCheck {

  chosenLot? : LotResponse;

  constructor(private lotService : LotService, private selectedLotService : SelectedLotService) { }

  ngDoCheck(): void {
    this.chosenLot = this.selectedLotService.getSelectedLot();
  }
}
