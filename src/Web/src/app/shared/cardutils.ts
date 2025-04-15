import { Card } from "../models/card.model";

export class CardUtils {
  getCensoredNumber(card?: Card): string {
    if (!card) return "";
    
    const first4 = card.cardNumber.toString().slice(0, 4);
    const totalCharAmount = card.cardNumber.toString().length - 4;
    const hiddenChars = "X".repeat(totalCharAmount);

    return `${first4}${hiddenChars}`.replace(/(.{4})/g, "$1 ").trim();
  }
  getExpirationDate(card?: Card): string {  
    if (!card) return "";
    
    const date = new Date(card.expireDate);
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    return `${month}/${year}`;
  }

  getCardIcon(card?: Card): string {
    if (!card) return "";
    
    const flag = card.cardFlag.toLowerCase().replaceAll(" ", "-");
    return `assets/icons/cards/${flag}.svg`;
  }
}
