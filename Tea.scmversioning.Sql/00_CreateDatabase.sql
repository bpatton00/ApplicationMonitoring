USE [master]
GO

/****** Object:  Database [ApplicationVersions]    Script Date: 7/26/2018 10:46:54 AM ******/
CREATE DATABASE [ApplicationVersions]
 CONTAINMENT = NONE
GO

ALTER DATABASE [ApplicationVersions] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ApplicationVersions].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ApplicationVersions] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ApplicationVersions] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ApplicationVersions] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ApplicationVersions] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ApplicationVersions] SET ARITHABORT OFF 
GO

ALTER DATABASE [ApplicationVersions] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ApplicationVersions] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ApplicationVersions] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ApplicationVersions] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ApplicationVersions] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ApplicationVersions] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ApplicationVersions] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ApplicationVersions] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ApplicationVersions] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ApplicationVersions] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ApplicationVersions] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ApplicationVersions] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ApplicationVersions] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ApplicationVersions] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ApplicationVersions] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ApplicationVersions] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ApplicationVersions] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ApplicationVersions] SET RECOVERY FULL 
GO

ALTER DATABASE [ApplicationVersions] SET  MULTI_USER 
GO

ALTER DATABASE [ApplicationVersions] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ApplicationVersions] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ApplicationVersions] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ApplicationVersions] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ApplicationVersions] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ApplicationVersions] SET  READ_WRITE 
GO

