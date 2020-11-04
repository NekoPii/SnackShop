-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: webdb
-- ------------------------------------------------------
-- Server version	5.7.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `mem_phone` char(11) NOT NULL,
  `mem_address` varchar(100) NOT NULL,
  `address_tag` varchar(45) NOT NULL,
  PRIMARY KEY (`mem_phone`,`mem_address`),
  CONSTRAINT `address_mem_fk` FOREIGN KEY (`mem_phone`) REFERENCES `members` (`mem_phone`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES ('18787890426','scsc','家');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goods`
--

DROP TABLE IF EXISTS `goods`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `goods` (
  `goods_id` int(11) NOT NULL AUTO_INCREMENT,
  `goods_name` varchar(100) NOT NULL,
  `goods_price` float NOT NULL,
  `goods_detail` varchar(5000) DEFAULT NULL,
  `goods_img_path` varchar(1000) DEFAULT NULL,
  `goods_tag` varchar(60) NOT NULL,
  PRIMARY KEY (`goods_id`),
  KEY `tag_fk_idx` (`goods_tag`),
  CONSTRAINT `tag_fk` FOREIGN KEY (`goods_tag`) REFERENCES `goods_tag` (`tag`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10000012 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goods`
--

LOCK TABLES `goods` WRITE;
/*!40000 ALTER TABLE `goods` DISABLE KEYS */;
INSERT INTO `goods` VALUES (10000000,'云龙县诺邓盐泥火腿',298,'诺邓火腿产区土壤有机质含量高，疏松、透气、排水良好；草山草场资源丰富水资源丰富，周边无污染，水质优良；属大陆性亚热带高原季风气候。诺邓火腿滋味品鉴口感鲜美，咸淡适中，香而回甜，回味悠长。','/content/image/product1/01.jpg','肉制品'),(10000001,'云龙县五谷粗粮高端礼盒',150,'粗杂粮有利于糖尿病用粗杂粮代替部分细粮有助于糖尿病患者控制血糖 研究表明，进食粗杂粮及杂豆类后的餐后血糖变化一般小于小麦和普通稻米，利于糖尿病病人血糖控制。国外一些糖尿病膳食指导组织已建议糖尿病病人尽量选择食用粗杂粮及杂豆类，可将它们作为主食或主食的一部分食用。但是这些粗杂粮和杂豆类维持餐后血糖反应的能力也是不同的。如燕麦、荞麦、大麦、红米、黑米、赤小豆、扁豆等可明显缓解糖尿病病人餐后高血糖状态，减少24小时内血糖波动，降低空腹血糖，减少胰岛素分泌，利于糖尿病病人的血糖控制。\r\n ','/content/image/product2/01.jpg','粗粮'),(10000002,'云龙县党参',3682,'党参味甘，性平。有补中益气、止渴、健脾益肺，养血生津。用于脾肺气虚，食少倦怠，咳嗽虚喘，气血不足，面色萎黄，心悸气短，津伤口渴，内热消渴。  懒言短气、四肢无力、食欲不佳、气虚、气津两虚、气血双亏以及血虚萎黄等症。但表证未解而中满邪实的不能用。 该品功效与人参相似，惟药力薄弱。治一般虚证，可代替人参使用；虚脱重证，则仍用人参为宜。','/content/image/product3/01.jpg','保健品'),(10000003,'云龙县石墨苦荞粉',30,'苦荞即苦荞麦，学名鞑靼荞麦（鞑靼，读音dádá，是对中国古代北方少数民族的统称），别名荞叶七、野兰荞、万年荞、 菠麦、乌麦、花荞。比甜荞即荞麦的营养价值高出很多。特别是生物类黄酮的含量是荞麦的13.5倍。','/content/image/product4/01.jpg','粗粮'),(10000004,'云龙县油牛肝菌',58,'牛肝菌味道鲜美，营养丰富。该菌菌体较大，肉肥厚，柄粗壮，食味香甜可口，营养丰富，是一种世界性著名食用菌。西欧各国也有广泛食用白牛肝菌的习惯，除新鲜的作菜外，大部分切片干燥，加工成各种小包装，用来配制汤料或做成酱油浸膏，也有制成盐腌品食用。','/content/image/product5/01.jpg','野生菌'),(10000005,'云龙县油松茸',58,'松茸，学名松口蘑。松茸对生长环境的要求非常苛刻，它只能生长在高寒且没有任何污染和人为干预的原始森林中，非常稀有。被誉为“菌中之王”。','/content/image/product6/01.jpg','野生菌'),(10000006,'云龙县野生黑木耳',58,'黑木耳为我国珍贵的药食兼用胶质真菌，也是世界上公认的保健食品。我国是黑木耳的故乡，中华民族早在4000多年前的神农氏时代便认识、开发了黑木耳，并开始栽培、食用。《礼记》中也有关于帝王宴会上食用黑木耳的记载。','/content/image/product7/01.jpg','野生菌'),(10000007,'云龙县苗尾小红米',12,'红米含有丰富的淀粉与植物蛋白质，可补充消耗的体力及维持身体正常体温。它富含众多的营养素，其中以铁质最为丰富，故有补血及预防贫血的功效。而其内含丰富的磷，维生素A、B群，则能改善营养不良、夜盲症和脚气病等毛病；又能有效舒缓疲劳、精神不振和失眠等症状。','/content/image/product8/01.jpg','粗粮'),(10000008,'云龙县土碱面条',12,'柠檬、橘子等酸性水果，或是食用的醋以及有机酸进入体内后，经过胰液、胆汁、肠液、碳酸的中和后被肝脏吸收，很快燃烧成二氧化碳，对身体不会造成负担。有机酸被分解后，留下的矿物质成分即钾、钠、钙、镁等。所以，消化功能不良的人应该尽量避免过度摄取酸性食物，尤其要减少蛋白质、脂肪、淀粉类食物的摄取，增加碱性食物的比例。','/content/image/product9/01.jpg','面食'),(10000009,'云龙县乳扇',42,'乳扇是鲜牛奶煮沸混合三比一的食用酸炼制凝结，制为薄片，缠绕于细竿上晾干而成。应是一种特形干酪。乳扇可作各种菜肴，凉拌、油煎、烧烤皆可。其名菜夹沙乳扇膨酥，入口即化。其他套炸、椒盐都别具一格。','/content/image/product10/01.jpg','奶制品'),(10000010,'云龙县松花糕',48,'松花粉中含有人体所需的 200 多种营养元素。其中 22 种 氨 基 酸、 15 种维生素 、 30 多种矿物质、 100 多种酶，以及核酸、不饱和脂肪酸、卵磷脂、类黄铜、单糖、多糖等。松花粉的营养成分不仅种类全面，且含量也非常高。其中蛋白质总含量是牛肉鸡蛋的 7 到 10 倍;铁比菠菜高出 20 倍; v a 原型胡萝卜素比胡萝卜多 20 到 30 倍。','/content/image/product11/01.jpg','糕点'),(10000011,'云龙县米糕',15,'松花粉中含有人体所需的 200 多种营养元素。其中 22 种 氨 基 酸、 15 种维生素 、 30 多种矿物质、 100 多种酶，以及核酸、不饱和脂肪酸、卵磷脂、类黄铜、单糖、多糖等。松花粉的营养成分不仅种类全面，且含量也非常高。其中蛋白质总含量是牛肉鸡蛋的 7 到 10 倍;铁比菠菜高出 20 倍; v a 原型胡萝卜素比胡萝卜多 20 到 30 倍。','/content/image/product12/01.jpg','糕点');
/*!40000 ALTER TABLE `goods` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goods_img`
--

DROP TABLE IF EXISTS `goods_img`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `goods_img` (
  `goods_id` int(11) NOT NULL,
  `img_path` varchar(1000) NOT NULL,
  PRIMARY KEY (`goods_id`,`img_path`),
  CONSTRAINT `img_to_goods` FOREIGN KEY (`goods_id`) REFERENCES `goods` (`goods_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goods_img`
--

LOCK TABLES `goods_img` WRITE;
/*!40000 ALTER TABLE `goods_img` DISABLE KEYS */;
INSERT INTO `goods_img` VALUES (10000000,'/content/image/product1/01.jpg'),(10000000,'/content/image/product1/02.jpg'),(10000000,'/content/image/product1/03.jpg'),(10000000,'/content/image/product1/04.jpg'),(10000000,'/content/image/product1/05.jpg'),(10000000,'/content/image/product1/06.jpg'),(10000001,'/content/image/product2/01.jpg'),(10000001,'/content/image/product2/02.jpg'),(10000001,'/content/image/product2/03.jpg'),(10000001,'/content/image/product2/04.jpg'),(10000001,'/content/image/product2/05.jpg'),(10000001,'/content/image/product2/06.jpg'),(10000002,'/content/image/product3/01.jpg'),(10000002,'/content/image/product3/02.jpg'),(10000002,'/content/image/product3/03.jpg'),(10000002,'/content/image/product3/04.jpg'),(10000002,'/content/image/product3/05.jpg'),(10000002,'/content/image/product3/06.jpg'),(10000003,'/content/image/product4/01.jpg'),(10000003,'/content/image/product4/02.jpg'),(10000003,'/content/image/product4/03.jpg'),(10000003,'/content/image/product4/04.jpg'),(10000004,'/content/image/product5/01.jpg'),(10000004,'/content/image/product5/02.jpg'),(10000004,'/content/image/product5/03.jpg'),(10000005,'/content/image/product6/01.jpg'),(10000005,'/content/image/product6/02.jpg'),(10000005,'/content/image/product6/03.jpg'),(10000005,'/content/image/product6/04.jpg'),(10000006,'/content/image/product7/01.jpg'),(10000006,'/content/image/product7/02.jpg'),(10000006,'/content/image/product7/03.jpg'),(10000006,'/content/image/product7/04.jpg'),(10000007,'/content/image/product8/01.jpg'),(10000007,'/content/image/product8/02.jpg'),(10000007,'/content/image/product8/03.jpg'),(10000008,'/content/image/product9/01.jpg'),(10000008,'/content/image/product9/02.jpg'),(10000008,'/content/image/product9/03.jpg'),(10000008,'/content/image/product9/04.jpg'),(10000009,'/content/image/product10/01.jpg'),(10000009,'/content/image/product10/02.jpg'),(10000009,'/content/image/product10/03.jpg'),(10000009,'/content/image/product10/04.jpg'),(10000010,'/content/image/product11/01.jpg'),(10000010,'/content/image/product11/02.jpg'),(10000010,'/content/image/product11/03.jpg'),(10000010,'/content/image/product11/04.jpg'),(10000011,'/content/image/product12/01.jpg'),(10000011,'/content/image/product12/02.jpg'),(10000011,'/content/image/product12/03.jpg'),(10000011,'/content/image/product12/04.jpg');
/*!40000 ALTER TABLE `goods_img` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `goods_tag`
--

DROP TABLE IF EXISTS `goods_tag`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `goods_tag` (
  `tag` varchar(60) NOT NULL,
  PRIMARY KEY (`tag`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `goods_tag`
--

LOCK TABLES `goods_tag` WRITE;
/*!40000 ALTER TABLE `goods_tag` DISABLE KEYS */;
INSERT INTO `goods_tag` VALUES ('保健品'),('奶制品'),('粗粮'),('糕点'),('肉制品'),('野生菌'),('面食');
/*!40000 ALTER TABLE `goods_tag` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `members`
--

DROP TABLE IF EXISTS `members`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `members` (
  `mem_phone` char(11) NOT NULL,
  `mem_pwd` varchar(45) NOT NULL,
  `mem_name` varchar(100) NOT NULL,
  `mem_type` int(11) NOT NULL,
  PRIMARY KEY (`mem_phone`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `members`
--

LOCK TABLES `members` WRITE;
/*!40000 ALTER TABLE `members` DISABLE KEYS */;
INSERT INTO `members` VALUES ('13022223333','pwd','mjx4',2),('13300001111','pwd','mjx2',1),('13322220000','pwd','mjx',1),('18787890426','pwd','MJX',2),('19946254180','pwd','mjx2',2);
/*!40000 ALTER TABLE `members` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchasecars`
--

DROP TABLE IF EXISTS `purchasecars`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchasecars` (
  `pcar_mem_phone` char(11) NOT NULL,
  `pcar_goods_id` int(11) NOT NULL,
  `pcar_goods_num` int(10) NOT NULL,
  PRIMARY KEY (`pcar_mem_phone`,`pcar_goods_id`),
  KEY `to_goods_idx` (`pcar_goods_id`),
  CONSTRAINT `pcar_mem_phone_fk` FOREIGN KEY (`pcar_mem_phone`) REFERENCES `members` (`mem_phone`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `pcar_to_goods` FOREIGN KEY (`pcar_goods_id`) REFERENCES `goods` (`goods_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchasecars`
--

LOCK TABLES `purchasecars` WRITE;
/*!40000 ALTER TABLE `purchasecars` DISABLE KEYS */;
INSERT INTO `purchasecars` VALUES ('18787890426',10000000,2),('18787890426',10000001,22223);
/*!40000 ALTER TABLE `purchasecars` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `purchaselists`
--

DROP TABLE IF EXISTS `purchaselists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchaselists` (
  `plist_id` varchar(45) NOT NULL,
  `plist_mem_phone` char(11) NOT NULL,
  `plist_goods_id` int(11) NOT NULL,
  `plist_goods_name` varchar(100) NOT NULL,
  `plist_goods_num` int(10) NOT NULL,
  `plist_goods_unit_price` float NOT NULL,
  `plist_goods_total_price` float NOT NULL,
  `plist_date` datetime NOT NULL,
  `plist_seller_phone` char(11) NOT NULL,
  PRIMARY KEY (`plist_id`,`plist_mem_phone`,`plist_goods_id`,`plist_date`),
  KEY `idx_composition` (`plist_mem_phone`,`plist_goods_id`),
  KEY `plist_seller_fk_idx` (`plist_seller_phone`),
  CONSTRAINT `mem_fk` FOREIGN KEY (`plist_mem_phone`) REFERENCES `members` (`mem_phone`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `plist_seller_fk` FOREIGN KEY (`plist_seller_phone`) REFERENCES `seller` (`seller_phone`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchaselists`
--

LOCK TABLES `purchaselists` WRITE;
/*!40000 ALTER TABLE `purchaselists` DISABLE KEYS */;
INSERT INTO `purchaselists` VALUES ('2020110400023674418787890426','18787890426',10000001,'云龙县五谷粗粮高端礼盒',1,150,150,'2020-11-04 00:02:37','18787890426'),('2020110400035338418787890426','18787890426',10000012,'sc',1,22,22,'2020-11-04 00:03:53','18787890426'),('2020110400081384418787890426','18787890426',10000012,'sc',1,22,22,'2020-11-04 00:08:14','18787890426');
/*!40000 ALTER TABLE `purchaselists` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sell_goods`
--

DROP TABLE IF EXISTS `sell_goods`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sell_goods` (
  `seller_phone` char(11) NOT NULL,
  `sell_goods_id` int(11) NOT NULL,
  `sell_volume` int(11) NOT NULL,
  `sell_stock` int(11) NOT NULL,
  PRIMARY KEY (`seller_phone`,`sell_goods_id`),
  KEY `sell_to_goods_idx` (`sell_goods_id`),
  CONSTRAINT `sell_to_goods` FOREIGN KEY (`sell_goods_id`) REFERENCES `goods` (`goods_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `to_seller_fk` FOREIGN KEY (`seller_phone`) REFERENCES `seller` (`seller_phone`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sell_goods`
--

LOCK TABLES `sell_goods` WRITE;
/*!40000 ALTER TABLE `sell_goods` DISABLE KEYS */;
INSERT INTO `sell_goods` VALUES ('18787890426',10000000,0,200),('18787890426',10000001,2,198),('18787890426',10000002,0,45),('18787890426',10000003,88,0),('18787890426',10000004,0,20),('19946254180',10000005,0,300),('19946254180',10000006,0,300),('19946254180',10000007,1,299),('19946254180',10000008,0,300),('19946254180',10000009,0,300),('19946254180',10000010,0,300),('19946254180',10000011,0,200);
/*!40000 ALTER TABLE `sell_goods` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seller`
--

DROP TABLE IF EXISTS `seller`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `seller` (
  `seller_phone` char(11) NOT NULL,
  `seller_count` varchar(45) NOT NULL,
  `seller_address` varchar(45) NOT NULL,
  PRIMARY KEY (`seller_phone`),
  CONSTRAINT `seller_phone` FOREIGN KEY (`seller_phone`) REFERENCES `members` (`mem_phone`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seller`
--

LOCK TABLES `seller` WRITE;
/*!40000 ALTER TABLE `seller` DISABLE KEYS */;
INSERT INTO `seller` VALUES ('13022223333','nekomjx@qq.com','幻想乡'),('18787890426','nekomjx@qq.com','幻想乡'),('19946254180','nekomjx@qq.com','红魔馆');
/*!40000 ALTER TABLE `seller` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wishlists`
--

DROP TABLE IF EXISTS `wishlists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `wishlists` (
  `wish_mem_phone` char(11) NOT NULL,
  `wish_goods_id` int(11) NOT NULL,
  PRIMARY KEY (`wish_goods_id`,`wish_mem_phone`),
  KEY `wish_mem_phone_fk_idx` (`wish_mem_phone`),
  CONSTRAINT `wish_mem_phone_fk` FOREIGN KEY (`wish_mem_phone`) REFERENCES `members` (`mem_phone`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `wish_to_goods` FOREIGN KEY (`wish_goods_id`) REFERENCES `goods` (`goods_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wishlists`
--

LOCK TABLES `wishlists` WRITE;
/*!40000 ALTER TABLE `wishlists` DISABLE KEYS */;
INSERT INTO `wishlists` VALUES ('18787890426',10000002),('18787890426',10000008);
/*!40000 ALTER TABLE `wishlists` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-11-04 11:45:32
