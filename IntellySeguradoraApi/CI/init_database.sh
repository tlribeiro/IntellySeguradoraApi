for i in `find /home/database/ -name "*.sql" | sort --version-sort`;
do mysql -udocker -pdocker docker_tlr_db < $i; done;