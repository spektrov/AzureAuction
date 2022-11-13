import {AfterContentInit, AfterViewInit, Component, DoCheck, OnInit} from '@angular/core';
import {LotService} from "../services/lot.service";
import {SelectedLotService} from "../services/selected-lot.service";
import {LotResponse} from "../responses/lot-response";
import {BlobService} from "../services/blob.service";

@Component({
  selector: 'app-lot-view',
  templateUrl: './lot-view.component.html',
  styleUrls: ['./lot-view.component.css']
})
export class LotViewComponent implements DoCheck {

  blobsUri = new Array<string>();
  timeLeft : number = 0;
  chosenLot? : LotResponse;

  constructor(private lotService : LotService,
              private selectedLotService : SelectedLotService,
              private blobService : BlobService) { }

  ngDoCheck(): void {
    this.chosenLot = this.selectedLotService.getSelectedLot();
    this.timeLeft = this.convertDate();
  }

  getPhotos()  {
    let id = this.chosenLot?.id!;
    this.blobService.getByLot(id).subscribe(photos => {
      this.blobsUri = photos;
    });
  }

  convertDate() : number {
    let start = new Date(this.chosenLot?.timeStart!);
    let end = new Date(this.chosenLot?.timeEnd!);
    let hours = Math.floor((end.valueOf() - start.valueOf()) / 1000 / 3600);
    return hours;
  }
}
