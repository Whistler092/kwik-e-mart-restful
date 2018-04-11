package eshop.kwikemart.controllers;

import eshop.kwikemart.models.TypeProduct;
import eshop.kwikemart.repository.TypeProductRepository;
import eshop.kwikemart.exception.ResourceNotFoundException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.util.List;
import java.util.Date;

@RestController
@RequestMapping("/api")
public class TypeProductController {

    @Autowired
    TypeProductRepository typeProductRepository;

    @GetMapping("/typeproducts")
    public List<TypeProduct> getAllTypeProducts() {
        return typeProductRepository.findAll();
    }

    @PostMapping("/typeproducts")
    public TypeProduct createTypeProduct(@Valid @RequestBody TypeProduct typeProduct) {
        typeProduct.setCreatedAt(new Date());
        typeProduct.setUpdatedAt(new Date());
        return typeProductRepository.save(typeProduct);
    }

    @GetMapping("/typeproducts/{id}")
    public TypeProduct getTypeProductById(@PathVariable(value = "id") Long idTypeProduct) {
        return typeProductRepository.findById(idTypeProduct)
                .orElseThrow(() -> new ResourceNotFoundException("TypeProduct", "id", idTypeProduct));
    }

    @PutMapping("/typeproducts/{id}")
    public TypeProduct updateTypeProduct(@PathVariable(value = "id") Long idTypeProduct,
                                           @Valid @RequestBody TypeProduct typeProductDetails) {

        TypeProduct typeProduct = typeProductRepository.findById(idTypeProduct)
                .orElseThrow(() -> new ResourceNotFoundException("TypeProduct", "id", idTypeProduct));

        typeProduct.setName(typeProductDetails.getName());

        TypeProduct updatedTypeProduct = typeProductRepository.save(typeProduct);
        return updatedTypeProduct;
    }

    @DeleteMapping("/typeproducts/{id}")
    public ResponseEntity<?> deleteTypeProduct(@PathVariable(value = "id") Long idTypeProduct) {
        TypeProduct typeProduct = typeProductRepository.findById(idTypeProduct)
                .orElseThrow(() -> new ResourceNotFoundException("TypeProduct", "id", idTypeProduct));

        typeProductRepository.delete(typeProduct);

        return ResponseEntity.ok().build();
    }
}