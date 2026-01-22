#!/bin/bash

# Docker deployment script for APIWEB

echo "🐳 APIWEB Docker Deployment Script"
echo "=================================="

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker first."
    exit 1
fi

# Function to build and run containers
build_and_run() {
    echo "🔧 Building Docker image..."
    docker-compose build
    
    echo "🚀 Starting containers..."
    docker-compose up -d
    
    echo "⏳ Waiting for services to be ready..."
    sleep 10
    
    echo "✅ Services are running!"
    echo ""
    echo "📍 Available endpoints:"
    echo "   API: http://localhost:5000"
    echo "   Swagger/OpenAPI: http://localhost:5000/openapi"
    echo "   PostgreSQL: localhost:5432"
    echo ""
    echo "🐘 Database connection:"
    echo "   Host: localhost"
    echo "   Port: 5432"
    echo "   Database: apiweb"
    echo "   Username: postgres"
    echo "   Password: postgres"
}

# Function to stop containers
stop() {
    echo "🛑 Stopping containers..."
    docker-compose down
    echo "✅ Containers stopped!"
}

# Function to clean up
clean() {
    echo "🧹 Cleaning up containers and images..."
    docker-compose down -v --rmi all
    docker system prune -f
    echo "✅ Cleanup completed!"
}

# Function to view logs
logs() {
    docker-compose logs -f
}

# Main menu
case "${1:-}" in
    "build")
        build_and_run
        ;;
    "stop")
        stop
        ;;
    "clean")
        clean
        ;;
    "logs")
        logs
        ;;
    "help"|"-h"|"--help"|"")
        echo "Usage: $0 [command]"
        echo ""
        echo "Commands:"
        echo "  build   - Build and start all services"
        echo "  stop    - Stop all services"
        echo "  clean   - Stop services and remove containers, images, and volumes"
        echo "  logs    - View container logs"
        echo "  help    - Show this help message"
        echo ""
        echo "Examples:"
        echo "  $0 build    # Build and run the application"
        echo "  $0 stop     # Stop the application"
        echo "  $0 logs     # View logs"
        ;;
    *)
        echo "❌ Unknown command: $1"
        echo "Run '$0 help' for available commands."
        exit 1
        ;;
esac