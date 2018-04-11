package eshop.kwikemart.controllers;

import eshop.kwikemart.models.*;
import eshop.kwikemart.repository.ProductRepository;
import eshop.kwikemart.exception.ResourceNotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;
import java.util.Date;

@RestController
@RequestMapping("/api")
public class ProductController {

    @Autowired
    ProductRepository productRepository;

    @GetMapping("/products")
    public List<Product> getAllProducts() {
        return productRepository.findAll();
    }

    @PostMapping("/products")
    public Product createProduct(@Valid @RequestBody Product product) {
        product.setCreatedAt(new Date());
        product.setUpdatedAt(new Date());
        return productRepository.save(product);
    }

    @GetMapping("/products/{id}")
    public Product getProductById(@PathVariable(value = "id") Long idProduct) {
        return productRepository.findById(idProduct)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "id", idProduct));
    }

    @PutMapping("/products/{id}")
    public Product updateProduct(@PathVariable(value = "id") Long idProduct,
                                           @Valid @RequestBody Product productDetails) {

        Product product = productRepository.findById(idProduct)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "id", idProduct));

        product.setName(productDetails.getName());
        product.setValue(productDetails.getValue());

        Product updatedProduct = productRepository.save(product);
        return updatedProduct;
    }

    @DeleteMapping("/products/{id}")
    public ResponseEntity<?> deleteProduct(@PathVariable(value = "id") Long idProduct) {
        Product product = productRepository.findById(idProduct)
                .orElseThrow(() -> new ResourceNotFoundException("Product", "id", idProduct));

        productRepository.delete(product);

        return ResponseEntity.ok().build();
    }
}