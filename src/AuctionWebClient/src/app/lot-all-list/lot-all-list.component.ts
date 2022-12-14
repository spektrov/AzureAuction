import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {LotService} from "../services/lot.service";
import {BlobService} from "../services/blob.service";

@Component({
  selector: 'app-lot-all-list',
  templateUrl: './lot-all-list.component.html',
  styleUrls: ['./lot-all-list.component.css']
})
export class LotAllListComponent implements AfterViewInit {
  displayedColumns: string[] = [ 'name', 'maxPrice', 'timeEnd' ];
  dataSource : MatTableDataSource<LotResponse>;
  blobsUri = new Array<string>();
  selectedLot? : LotResponse;
  bidPrice? : number;
  errorBid : Boolean = false;

  constructor(private lotService : LotService,
              private blobService : BlobService,
              ) {
    this.dataSource = new MatTableDataSource<LotResponse>();
  }

  ngAfterViewInit() {
    this.updateList();
  }

  onSelect(lot: LotResponse): void {
    this.selectedLot = lot;
    this.bidPrice = lot.maxPrice;
    this.getPhotos();
    this.errorBid = false;
  }

  getPhotos()  {
    let id = this.selectedLot?.id!;
    this.blobService.getByLot(id).subscribe(photos => {
      this.blobsUri = photos;
    });
  }

  updateList() {
    this.lotService.getAllLots().subscribe(lots => {
      this.dataSource.data = lots;
    });
  }
}
