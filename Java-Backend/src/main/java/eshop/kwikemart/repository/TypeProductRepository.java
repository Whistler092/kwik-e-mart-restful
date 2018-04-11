package eshop.kwikemart.repository;

import eshop.kwikemart.models.TypeProduct;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

/**
 * Created by Alex Mauricio Paredes Celorio on 08/04/18.
 */

@Repository
public interface TypeProductRepository extends JpaRepository<TypeProduct, Long> {

}