import { Card } from "../models/card.model";

export class CardUtils {
  getCensoredNumber(card?: Card): string {
    if (!card) return "";
    
    const first4 = card.number.toString().slice(0, 4);
    const totalCharAmount = card.number.toString().length - 4;
    const hiddenChars = "X".repeat(totalCharAmount);

    return `${first4}${hiddenChars}`.replace(/(.{4})/g, "$1 ").trim();
  }
}
