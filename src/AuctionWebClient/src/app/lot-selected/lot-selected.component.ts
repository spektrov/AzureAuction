import {Component, Input, OnInit} from '@angular/core';
import {LotResponse} from "../responses/lot-response";


@Component({
  selector: 'app-lot-selected',
  templateUrl: './lot-selected.component.html',
  styleUrls: ['./lot-selected.component.css']
})
export class LotSelectedComponent implements OnInit {
  @Input() selectedLot? : LotResponse;
  @Input() blobsUri = new Array<string>();

  constructor() {}

  ngOnInit(): void {
  }

  convertDate() : number {
    let start = new Date(this.selectedLot?.timeStart!);
    let end = new Date(this.selectedLot?.timeEnd!);
    let hours = Math.floor((end.valueOf() - start.valueOf()) / 1000 / 3600);
    return hours;
  }
}
