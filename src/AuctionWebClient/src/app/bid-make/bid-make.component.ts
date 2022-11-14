import {Component, Input, OnInit} from '@angular/core';
import {BidRequest} from "../requests/bid-request";
import {LotResponse} from "../responses/lot-response";
import {BidService} from "../services/bid.service";
import {TokenService} from "../services/token.service";

@Component({
  selector: 'app-bid-make',
  templateUrl: './bid-make.component.html',
  styleUrls: ['./bid-make.component.css']
})
export class BidMakeComponent implements OnInit {
  @Input() selectedLot? : LotResponse;
  @Input() bidPrice? : number;

  isLoggedIn = false;
  errorBid : Boolean = false;

  constructor(private bidService : BidService, private tokenService : TokenService) {}

  ngOnInit(): void {
    let isLoggedIn = this.tokenService.isLoggedIn();
    if (isLoggedIn) {
      this.isLoggedIn = true;
    }
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
      },
      error: () => {this.errorBid = true;}
    });
  }
}
