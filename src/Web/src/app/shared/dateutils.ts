export class DateUtils {
  getDateName(date?: Date): string {
    date ??= new Date();

    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();

    const weekDays = [
      "domingo",
      "segunda-feira",
      "terça-feira",
      "quarta-feira",
      "quinta-feira",
      "sexta-feira",
      "sábado",
    ];

    const nameOfTheDay = weekDays[date.getDay()];

    return `${nameOfTheDay}, ${day}/${month}/${year}`;
  }

  getDateFormatted(dateInput?: string): string {
    if (!dateInput) return "";
    const date = new Date(dateInput);

    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();

    return `${day}/${month}/${year}`;
  }

  getHourFormatted(dateInput?: string): string {
    if (!dateInput) return "";

    const date = new Date(dateInput);

    const hours = String(date.getHours()).padStart(2, "0");
    const minutes = String(date.getMinutes()).padStart(2, "0");

    return `${hours}:${minutes}`;
  }
}
