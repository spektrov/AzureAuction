import {AfterViewInit, Component, EventEmitter, Input, Output, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {LotResponse} from "../responses/lot-response";
import {MatPaginator} from "@angular/material/paginator";
import {MatSort} from "@angular/material/sort";

@Component({
  selector: 'app-lot-table',
  templateUrl: './lot-table.component.html',
  styleUrls: ['./lot-table.component.css']
})
export class LotTableComponent implements AfterViewInit {

  @Input() displayedColumns!: string[];
  @Input() dataSource! : MatTableDataSource<LotResponse>;

  @Input() selectedLot? : LotResponse;
  @Output() onSelectEvent = new EventEmitter<LotResponse>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {
  }

  ngAfterViewInit() {
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

  onSelect(lot: LotResponse) {
    this.onSelectEvent.emit(lot);
  }
}
