import {AfterViewInit, Component, OnInit, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {LotService} from "../services/lot.service";
import {Guid} from "guid-typescript";
import {SelectedLotService} from "../services/selected-lot.service";
import {BlobService} from "../services/blob.service";
import {UserService} from "../services/user.service";
import {BidService} from "../services/bid.service";
import {BidRequest} from "../requests/bid-request";
import {catchError} from "rxjs";
import {Router} from "@angular/router";

@Component({
  selector: 'app-lot-all-list',
  templateUrl: './lot-all-list.component.html',
  styleUrls: ['./lot-all-list.component.css']
})
export class LotAllListComponent implements AfterViewInit {
  displayedColumns: string[] = ['id', 'name', 'maxPrice', 'timeEnd',  'category.name' ];
  dataSource : MatTableDataSource<LotResponse>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  slides: any[] = new Array(3).fill({id: -1, src: '', title: '', subtitle: ''});

  blobsUri = new Array<string>();
  selectedLot? : LotResponse;
  bidPrice? : number;
  errorBid : Boolean = false;
  href : string = '';

  constructor(private lotService : LotService,
              private blobService : BlobService,
              private bidService : BidService,
              ) {
    this.dataSource = new MatTableDataSource<LotResponse>();
    this.href = 'lots/all';
  }

  ngAfterViewInit() {
    this.updateList();

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
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

    this.slides[0] = {
      src: this.blobsUri[0],
    };
    this.slides[1] = {
      src: this.blobsUri[0],
    }
    this.slides[2] = {
      src: this.blobsUri[0],
    }
  }

  convertDate() : number {
    let start = new Date(this.selectedLot?.timeStart!);
    let end = new Date(this.selectedLot?.timeEnd!);
    let hours = Math.floor((end.valueOf() - start.valueOf()) / 1000 / 3600);
    return hours;
  }

  updateList() {
    this.lotService.getAllLots().subscribe(lots => {
      this.dataSource.data = lots;
    });
  }

  makeBid() {
    let request : BidRequest = {
      lotId :  this.selectedLot!.id,
      price : this.bidPrice!,
      ts : new Date()
    }

    this.bidService.createBid(request).subscribe({
      next: response => {
        this.errorBid = false;
        this.selectedLot!.maxPrice = request.price;
        this.updateList();
      },
      error: () => {this.errorBid = true;}
    });
  }
}
