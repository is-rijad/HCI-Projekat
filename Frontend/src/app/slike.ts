export class Slike {
  static nizSlika = [
    "assets/hotelSlike/pexels-andrea-davis-2873951.jpg",
    "assets/hotelSlike/pexels-nastyasensei-331107.jpg",
    "assets/hotelSlike/pexels-pixabay-53577.jpg",
    "assets/hotelSlike/pexels-pixabay-164595.jpg",
    "assets/hotelSlike/pexels-pixabay-258154.jpg",
    "assets/hotelSlike/pexels-pixabay-261102.jpg",
    "assets/hotelSlike/pexels-pixabay-271624.jpg",
    "assets/hotelSlike/pexels-pixabay-271631.jpg",
    "assets/hotelSlike/pexels-quark-studio-2507007.jpg",
    "assets/hotelSlike/pexels-thorsten-technoman-338504.jpg"
  ]
  static getRandomSliku() {
    let index = Math.floor(Math.random() * this.nizSlika.length);
    return this.nizSlika[index];
  }

}
