import {AfterViewInit, Component, OnInit} from '@angular/core';
import {LotService} from "../services/lot.service";
import {CategoryService} from "../services/category.service";
import {Observable} from "rxjs";
import {Category} from "../models/category";
import {LotRequest} from "../requests/lot-request";
import {FormControl, FormGroup} from "@angular/forms";
import {Guid} from "guid-typescript";
import {UploadFileRequest} from "../requests/upload-file-request";
import {BlobService} from "../services/blob.service";
import {LotResponse} from "../responses/lot-response";

@Component({
  selector: 'app-lot-add',
  templateUrl: './lot-add.component.html',
  styleUrls: ['./lot-add.component.css']
})
export class LotAddComponent implements AfterViewInit {
  private lotId : string = '';

  isSuccessful : boolean = false;

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  categories? : Category[];

  filenames : string[] = new Array<string>();

  imageSource = '';

  lotToAdd : LotRequest = {
    id : (Guid.create()).toString(),
    name : '',
    description : '',
    timeStart : new Date(),
    timeEnd : new Date(),
    startPrice : 0,
    categoryId : Guid.createEmpty(),
  };

  constructor(private lotService : LotService,
              private categoryService : CategoryService,
              private blobService : BlobService) { }

  ngAfterViewInit(): void {
    this.categoryService.getAllCategories().subscribe(c => {
      this.categories = c;
    });
  }

  setFilename(files : any) {
    for (let i = 0; i < files.length; i++) {
      this.filenames[i] = files[i].name;

      console.log(this.filenames[i]);
    }
  }

  addLot(lot : LotRequest, files : any) : void {
    this.lotService.addLot(lot)
      .subscribe(data => this.lotId = data );

    this.isSuccessful = true;

    console.log(files.length);

    const formData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append(files[i].name, files[i]);
    }
    this.blobService.upload(formData, this.lotToAdd.id).subscribe();
  }
}
