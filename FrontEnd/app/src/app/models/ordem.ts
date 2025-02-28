import { cliente } from "./cliente";
import { ItensOrder } from "./ItensOrder";
import { Produto } from "./produto";

export class Ordem {
  id: number | undefined;
  ClientId: number | undefined;
  TotalPrice: number | undefined;
  ItensP : ItensOrder[] = [];
}




