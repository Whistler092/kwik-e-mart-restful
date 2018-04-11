package eshop.kwikemart.repository;

import eshop.kwikemart.models.Product;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

/**
 * Created by rajeevkumarsingh on 27/06/17.
 */

@Repository
public interface ProductRepository extends JpaRepository<Product, Long> {

}