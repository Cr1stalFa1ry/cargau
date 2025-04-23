#!/bin/bash

# Пути к проектам
PROJECT_PATH="../db"
STARTUP_PROJECT_PATH="../CARGAU"

# Цвета
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m' # No Color

print_usage() {
  echo -e "${GREEN}EF CLI утилита:${NC}"
  echo "  ./ef-migrations.sh create <MigrationName>   - Создать и применить миграцию"
  echo "  ./ef-migrations.sh remove                   - Удалить последнюю миграцию"
  echo "  ./ef-migrations.sh update                   - Применить все миграции к БД"
  echo "  ./ef-migrations.sh drop                     - Удалить БД"
}

# --- Основная логика ---
COMMAND=$1
MIGRATION_NAME=$2

case "$COMMAND" in
  create)
    if [ -z "$MIGRATION_NAME" ]; then
      echo -e "${RED}❗ Укажи название миграции.${NC}"
      echo "👉 Пример: ./ef-migrations.sh create AddCarsTable"
      exit 1
    fi
    echo -e "${GREEN}📦 Создание миграции: $MIGRATION_NAME...${NC}"
    dotnet ef migrations add "$MIGRATION_NAME" --project "$PROJECT_PATH" --startup-project "$STARTUP_PROJECT_PATH"

    echo -e "${GREEN}🔄 Применение миграции...${NC}"
    dotnet ef database update --project "$PROJECT_PATH" --startup-project "$STARTUP_PROJECT_PATH"
    ;;
  
  remove)
    echo -e "${GREEN}🗑 Удаление последней миграции...${NC}"
    dotnet ef migrations remove --project "$PROJECT_PATH" --startup-project "$STARTUP_PROJECT_PATH"
    ;;
  
  update)
    echo -e "${GREEN}🔄 Применение всех миграций...${NC}"
    dotnet ef database update --project "$PROJECT_PATH" --startup-project "$STARTUP_PROJECT_PATH"
    ;;
  
  drop)
    echo -e "${RED}⚠️ Удаление БД. Ты уверен? (y/N)${NC}"
    read confirm
    if [[ "$confirm" == "y" || "$confirm" == "Y" ]]; then
      dotnet ef database drop --project "$PROJECT_PATH" --startup-project "$STARTUP_PROJECT_PATH"
    else
      echo "❎ Отменено."
    fi
    ;;
  
  *)
    print_usage
    ;;
esac
