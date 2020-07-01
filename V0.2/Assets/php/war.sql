CREATE TABLE `user` (
  `id` int(11) NOT NULL auto_increment,
  `login` varchar(50) NOT NULL,
  `password` longtext NOT NULL,
  `nick` varchar(50) NOT NULL,
  `created_at` timestamp  NOT NULL,
  `updated_at` timestamp NULL,
  `type` tinyint(1) default '0',
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

CREATE TABLE `achievement` (
  `id` int(11) NOT NULL auto_increment,
  
  `achievement` boolean NOT NULL,
  `description` boolean NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

CREATE TABLE `achievement_user` (
  `id` int(11) NOT NULL auto_increment,
  `id_user` int(11) NOT NULL,
  `id_achievement` int(11) NOT NULL,
  
  `created_at` timestamp NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

CREATE TABLE `game` (
  `id` int(11) NOT NULL auto_increment,
  `id_user` int(11) NOT NULL,
  `win` boolean NOT NULL,
  `score` int(11) NOT NULL,
  `created_at` timestamp NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;







CREATE TABLE `game_enemy` (
  `id` int(11) NOT NULL auto_increment,
  `id_game` int(11) NOT NULL,
  `enemy` int(11) NOT NULL,
  PRIMARY KEY  (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8 AUTO_INCREMENT=1;


