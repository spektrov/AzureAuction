import {Component, Input, OnInit} from '@angular/core';
import {LotService} from "../services/lot.service";
import {LotResponse} from "../responses/lot-response";

@Component({
  selector: 'app-lot-delete',
  templateUrl: './lot-delete.component.html',
  styleUrls: ['./lot-delete.component.css']
})
export class LotDeleteComponent implements OnInit {
  @Input() selectedLot? : LotResponse;
  @Input() isSuccessful? : number;

  constructor(private lotService : LotService) { }

  ngOnInit(): void {
  }

  onDelete() {
    let id = this.selectedLot?.id!;
    this.lotService.deleteLot(id).subscribe({
      next : () => this.isSuccessful = 1,
      error : () => this.isSuccessful = -1
    });
  }

  onOk () {
    this.isSuccessful = 0;
  }
}
