import {AfterViewInit, Component, OnInit} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {LotService} from "../services/lot.service";
import {BlobService} from "../services/blob.service";
import {TokenService} from "../services/token.service";
import {Guid} from "guid-typescript";

@Component({
  selector: 'app-lot-by-user-create',
  templateUrl: './lot-by-user-create.component.html',
  styleUrls: ['./lot-by-user-create.component.css']
})
export class LotByUserCreateComponent implements AfterViewInit {
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
      this.lotService.getUserLots(this.userId!).subscribe(lots => {
        this.dataSource.data = lots;
      });
    }
  }
}
