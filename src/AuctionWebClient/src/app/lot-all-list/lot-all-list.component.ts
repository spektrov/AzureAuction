import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";
import {LotService} from "../services/lot.service";

@Component({
  selector: 'app-lot-all-list',
  templateUrl: './lot-all-list.component.html',
  styleUrls: ['./lot-all-list.component.css']
})
export class LotAllListComponent implements AfterViewInit {
  displayedColumns: string[] = ['name', 'maxPrice', 'timeEnd',  'category.name' ];
  dataSource : MatTableDataSource<LotResponse>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private lotService : LotService) {
    this.dataSource = new MatTableDataSource<LotResponse>();
  }

  ngAfterViewInit() {
    this.lotService.getAllLots().subscribe(lots => {
      this.dataSource.data = lots;
    });

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

}
