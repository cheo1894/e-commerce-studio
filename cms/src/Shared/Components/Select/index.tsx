import "./index.css";
import type { CategoryEntity } from "@/Modules/Dashboard/Modules/Products/Domain/Entities/CategoryEntity";
type Props = {
  title: string;
  options: CategoryEntity[];
};

function Index({ options, title }: Props) {
  return (
    <div className="select-container">
      <h5 className="select-title">{title}</h5>
      <select className="select-body" id="color" name="color">
        <option value="">Seleccione...</option>
        {options &&
          options.map((o, index) => {
            return (
              <option key={index} value={o.categoryId}>
                {o.categoryName}
              </option>
            );
          })}
      </select>
    </div>
  );
}

export default Index;
