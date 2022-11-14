import {AfterViewInit, Component} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {Guid} from "guid-typescript";
import {LotService} from "../services/lot.service";
import {BlobService} from "../services/blob.service";
import {TokenService} from "../services/token.service";

@Component({
  selector: 'app-lot-by-user-buy',
  templateUrl: './lot-by-user-buy.component.html',
  styleUrls: ['./lot-by-user-buy.component.css']
})
export class LotByUserBuyComponent implements AfterViewInit {
  displayedColumns: string[] = [ 'name', 'maxPrice', 'timeEnd' ];
  dataSource : MatTableDataSource<LotResponse>;
  blobsUri = new Array<string>();
  selectedLot? : LotResponse;
  userId? : Guid;

  constructor(private lotService : LotService,
              private blobService : BlobService,
              private tokenService : TokenService) {
    this.dataSource = new MatTableDataSource<LotResponse>();
  }

  ngAfterViewInit() {
    let id =  this.tokenService.getSession()?.userId!;
    this.userId = Guid.parse(id);
    this.updateList();
  }

  onSelect(lot: LotResponse): void {
    this.selectedLot = lot;
    this.getPhotos();
  }

  getPhotos()  {
    let id = this.selectedLot?.id!;
    this.blobService.getByLot(id).subscribe(photos => {
      this.blobsUri = photos;
    });
  }

  updateList() {
    if (this.userId) {
      this.lotService.getUserBoughtLots(this.userId!).subscribe(lots => {
        this.dataSource.data = lots;
      });
    }
  }

}
