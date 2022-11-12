import {AfterViewInit, Component, OnInit} from '@angular/core';
import {LotService} from "../services/lot.service";
import {CategoryService} from "../services/category.service";
import {Observable} from "rxjs";
import {Category} from "../models/category";
import {LotRequest} from "../requests/lot-request";
import {FormControl, FormGroup} from "@angular/forms";
import {Guid} from "guid-typescript";

@Component({
  selector: 'app-lot-add',
  templateUrl: './lot-add.component.html',
  styleUrls: ['./lot-add.component.css']
})
export class LotAddComponent implements AfterViewInit {
  isSuccessful : boolean = false;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  categories? : Category[];

  lotToAdd : LotRequest = {
    name : '',
    description : '',
    timeStart : new Date(),
    timeEnd : new Date(),
    startPrice : 0,
    categoryId : Guid.createEmpty()
  };

  constructor(private lotService : LotService, private categoryService : CategoryService) { }

  ngAfterViewInit(): void {
    this.categoryService.getAllLots().subscribe(c => {
      this.categories = c;
    });
  }


  addLot(lot : LotRequest) : void {
    this.lotService.addLot(lot).subscribe(() => {
      this.isSuccessful = true;
    });
  }
}
